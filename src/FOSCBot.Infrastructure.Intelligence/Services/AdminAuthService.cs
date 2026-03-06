using System.Security.Cryptography;
using FOSCBot.Common.Persistence;
using FOSCBot.Common.Persistence.Entities;
using FOSCBot.Core.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace FOSCBot.Infrastructure.Intelligence.Services;

internal class AdminAuthService : IAdminAuthService
{
    private readonly IFosboDbContext _dbContext;
    private readonly IMemoryCache _cache;

    public AdminAuthService(IFosboDbContext dbContext, IMemoryCache cache)
    {
        _dbContext = dbContext;
        _cache = cache;
    }

    public string GenerateCode(long telegramUserId)
    {
        var code = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();

        _cache.Set(CacheKey(telegramUserId), code, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });

        return code;
    }

    public async Task<bool> VerifyCodeAsync(long telegramUserId, string code)
    {
        if (!_cache.TryGetValue(CacheKey(telegramUserId), out string? cached) || cached != code)
            return false;

        _cache.Remove(CacheKey(telegramUserId));

        var alreadyExists = await _dbContext.Masters
            .AnyAsync(m => m.TelegramUserExternalId == telegramUserId);

        if (!alreadyExists)
        {
            _dbContext.Masters.Add(new Master
            {
                Id = Guid.NewGuid(),
                TelegramUserExternalId = telegramUserId,
                AuthenticatedAt = DateTimeOffset.UtcNow
            });

            await _dbContext.SaveChangesAsync();
        }

        return true;
    }

    public async Task<bool> IsAdminAsync(long telegramUserId)
    {
        return await _dbContext.Masters
            .AnyAsync(m => m.TelegramUserExternalId == telegramUserId);
    }

    private static string CacheKey(long telegramUserId) =>
        $"auth.code:{telegramUserId}";
}
