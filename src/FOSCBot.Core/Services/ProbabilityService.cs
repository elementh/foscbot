using Microsoft.Extensions.Caching.Memory;

namespace FOSCBot.Core.Services;

public class ProbabilityService(IMemoryCache cache)
{
    private static readonly int TippingPoint = 10;

    public bool GetResult(string key)
    {
        var callCount = cache.GetOrCreate(key, entry => 0);
        callCount++;

        cache.Set(key, callCount);
        var probability = Math.Min(1.0, (double)callCount / (2 * TippingPoint));
        return new Random().NextDouble() < probability;
    }

    public void Reset(string key)
    {
        cache.Set(key, 0);
    }
}