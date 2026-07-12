using FOSCBot.Common.Persistence;
using FOSCBot.Common.Persistence.Entities;
using FOSCBot.Core.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FOSCBot.Infrastructure.Intelligence.Services;

internal class UserMemoryService : IUserMemoryService
{
    private readonly IFosboDbContext _dbContext;
    private readonly IMemoryCache _cache;
    private readonly UserMemoryChannel _channel;
    private readonly UserMemoryOptions _userMemoryOptions;
    private readonly ILogger<UserMemoryService> _logger;

    public UserMemoryService(IFosboDbContext dbContext, IMemoryCache cache, UserMemoryChannel channel,
        IOptions<UserMemoryOptions> userMemoryOptions, ILogger<UserMemoryService> logger)
    {
        _dbContext = dbContext;
        _cache = cache;
        _channel = channel;
        _userMemoryOptions = userMemoryOptions.Value;
        _logger = logger;
    }

    public async Task<UserMemory?> GetAsync(long telegramUserId)
    {
        var cacheKey = CacheKey(telegramUserId);

        if (_cache.TryGetValue(cacheKey, out UserMemory? cached))
            return cached;

        var memory = await _dbContext.UserMemories
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.TelegramUserExternalId == telegramUserId);

        if (memory is not null)
            _cache.Set(cacheKey, memory, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

        return memory;
    }

    public async Task<UserMemory?> GetOrCreateAsync(long telegramUserId)
    {
        var existing = await GetAsync(telegramUserId);
        if (existing is not null)
            return existing;

        var memory = new UserMemory
        {
            Id = Guid.NewGuid(),
            TelegramUserExternalId = telegramUserId,
            Content = string.Empty,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _dbContext.UserMemories.Add(memory);
        await _dbContext.SaveChangesAsync();

        _cache.Remove(CacheKey(telegramUserId));

        return memory;
    }

    public async Task SaveAsync(UserMemory memory)
    {
        var existing = await _dbContext.UserMemories
            .FirstOrDefaultAsync(m => m.TelegramUserExternalId == memory.TelegramUserExternalId);

        if (existing is null)
        {
            memory.Id = Guid.NewGuid();
            memory.CreatedAt = DateTimeOffset.UtcNow;
            memory.UpdatedAt = DateTimeOffset.UtcNow;
            _dbContext.UserMemories.Add(memory);
        }
        else
        {
            existing.Content = memory.Content;
            existing.UpdatedAt = DateTimeOffset.UtcNow;
        }

        await _dbContext.SaveChangesAsync();
        _cache.Remove(CacheKey(memory.TelegramUserExternalId));
    }

    public Task AccumulateMessageAsync(long chatId, long telegramUserId, string username, string text)
    {
        var cacheKey = BufferCacheKey(chatId);
        var buffer = _cache.GetOrCreate(cacheKey, entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(_userMemoryOptions.BufferSlidingExpirationMinutes);
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_userMemoryOptions.BufferAbsoluteExpirationMinutes);
            entry.RegisterPostEvictionCallback(OnBufferEvicted, chatId);
            return new List<AccumulatedMessage>();
        })!;

        lock (buffer)
        {
            buffer.Add(new AccumulatedMessage(telegramUserId, username, text, DateTimeOffset.UtcNow));
        }

        return Task.CompletedTask;
    }

    private static string CacheKey(long telegramUserId) => $"user.memory:{telegramUserId}";
    private static string BufferCacheKey(long chatId) => $"user.memory.buffer:{chatId}";

    private void OnBufferEvicted(object key, object? value, EvictionReason reason, object? state)
    {
        if (value is not List<AccumulatedMessage> buffer || buffer.Count == 0)
            return;

        if (state is not long chatId)
            return;

        var messages = buffer.ToList();

        if (!_channel.Channel.Writer.TryWrite(new MemoryBatch(chatId, messages)))
        {
            _logger.LogWarning(
                "Dropped memory batch for chat {ChatId} with {MessageCount} messages after eviction {Reason}",
                chatId,
                messages.Count,
                reason);
        }
    }
}
