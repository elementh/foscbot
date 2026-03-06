using FOSCBot.Common.Persistence;
using FOSCBot.Common.Persistence.Entities;
using FOSCBot.Core.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace FOSCBot.Infrastructure.Intelligence.Services;

internal class PhantomCommandService : IPhantomCommandService
{
    private readonly IFosboDbContext _dbContext;
    private readonly IMemoryCache _cache;

    public PhantomCommandService(IFosboDbContext dbContext, IMemoryCache cache)
    {
        _dbContext = dbContext;
        _cache = cache;
    }

    public async Task<PhantomCommand?> GetCommandAsync(string name, long chatExternalId)
    {
        var cacheKey = CacheKey(name, chatExternalId);

        if (_cache.TryGetValue(cacheKey, out PhantomCommand? cached))
            return cached;

        var command = await _dbContext.PhantomCommands
            .AsNoTracking()
            .FirstOrDefaultAsync(c =>
                c.Name == name &&
                c.Chats.Any(cc => cc.ChatExternalId == chatExternalId));

        if (command is not null)
            _cache.Set(cacheKey, command);

        return command;
    }

    public async Task<PhantomCommand> SaveCommandAsync(string name, string description, string personality, long chatExternalId)
    {
        var command = new PhantomCommand
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Personality = personality,
            CreatedAt = DateTimeOffset.UtcNow,
            Chats = [new PhantomCommandChat { ChatExternalId = chatExternalId }]
        };

        _dbContext.PhantomCommands.Add(command);
        await _dbContext.SaveChangesAsync();

        _cache.Set(CacheKey(name, chatExternalId), command);

        return command;
    }

    public async Task DeleteAllAsync()
    {
        var commands = await _dbContext.PhantomCommands
            .Include(c => c.Chats)
            .ToListAsync();

        _dbContext.PhantomCommands.RemoveRange(commands);
        await _dbContext.SaveChangesAsync();

        foreach (var cmd in commands)
        {
            foreach (var chat in cmd.Chats)
            {
                _cache.Remove(CacheKey(cmd.Name, chat.ChatExternalId));
            }
        }
    }

    private static string CacheKey(string name, long chatExternalId) =>
        $"phantom.cmd:{chatExternalId}:{name}";
}
