using FOSCBot.Core.Application.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace FOSCBot.Infrastructure.Intelligence.Services;

internal class YappingService(IMemoryCache cache) : IYappingService
{
    public bool IsYapping(long chatId)
    {
        return cache.TryGetValue(CacheKey(chatId), out bool yapping) && yapping;
    }

    public void Toggle(long chatId, TimeSpan duration)
    {
        var key = CacheKey(chatId);

        if (cache.TryGetValue(key, out bool yapping) && yapping)
        {
            cache.Remove(key);
        }
        else
        {
            cache.Set(key, true, duration);
        }
    }

    private static string CacheKey(long chatId) => $"yapping:{chatId}";
}
