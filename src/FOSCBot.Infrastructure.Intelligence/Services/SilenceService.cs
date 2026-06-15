using FOSCBot.Core.Application.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace FOSCBot.Infrastructure.Intelligence.Services;

internal class SilenceService(IMemoryCache cache) : ISilenceService
{
    public void Silence(long chatId, TimeSpan duration)
    {
        cache.Set(CacheKey(chatId), true, duration);
    }

    public void Unsilence(long chatId)
    {
        cache.Remove(CacheKey(chatId));
    }

    public bool IsSilenced(long chatId)
    {
        return cache.TryGetValue(CacheKey(chatId), out bool silenced) && silenced;
    }

    private static string CacheKey(long chatId) => $"silence:{chatId}";
}
