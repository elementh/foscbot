using Microsoft.Extensions.Caching.Memory;

namespace FOSCBot.Core.Services;

public class UnhingedService(IMemoryCache cache)
{
    public bool IsUnhinged(long chatId)
    {
        return cache.Get<bool>($"unhinged:{chatId}");
    }

    public void SetUnhinged(long chatId)
    {
        cache.Set($"unhinged:{chatId}", true);
    }

    public void Clear(long chatId)
    {
        cache.Remove($"fallback.catchall:{chatId}");
        cache.Remove($"unhinged:{chatId}");
    }
}