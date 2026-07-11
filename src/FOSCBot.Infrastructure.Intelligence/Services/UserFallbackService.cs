using FOSCBot.Common.Persistence;
using FOSCBot.Common.Persistence.Entities;
using FOSCBot.Core.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace FOSCBot.Infrastructure.Intelligence.Services;

internal class UserFallbackService : IUserFallbackService
{
    private readonly IFosboDbContext _dbContext;
    private readonly IMemoryCache _cache;

    public UserFallbackService(IFosboDbContext dbContext, IMemoryCache cache)
    {
        _dbContext = dbContext;
        _cache = cache;
    }

    public async Task<UserFallback?> GetAsync(long telegramUserId)
    {
        var cacheKey = CacheKey(telegramUserId);

        if (_cache.TryGetValue(cacheKey, out UserFallback? cached))
            return cached;

        var fallback = await _dbContext.UserFallbacks
            .AsNoTracking()
            .Include(f => f.Sentences)
            .FirstOrDefaultAsync(f => f.TelegramUserExternalId == telegramUserId);

        if (fallback is not null)
            _cache.Set(cacheKey, fallback, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

        return fallback;
    }

    private static string CacheKey(long telegramUserId) =>
        $"user.fallback:{telegramUserId}";
}
