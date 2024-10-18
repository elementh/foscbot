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

    public string? GetPrompt(long chatId)
    {
        return cache.Get<string?>($"unhinged.prompt:{chatId}");
    }

    public void SetPrompt(long chatId, string prompt)
    {
        cache.Set($"unhinged.prompt:{chatId}", prompt);
    }

    public void Clear(long chatId)
    {
        cache.Remove($"fallback.catchall:{chatId}");
        cache.Remove($"unhinged:{chatId}");
        cache.Remove($"unhinged.prompt:{chatId}");
        cache.Remove($"unhinged.temperature:{chatId}");
    }

    public double? GetTemperature(long chatId)
    {
        return cache.Get<double?>($"unhinged.temperature:{chatId}");
    }

    public void SetTemperature(long chatId, double temperature)
    {
        cache.Set($"unhinged.temperature:{chatId}", temperature);
    }
}