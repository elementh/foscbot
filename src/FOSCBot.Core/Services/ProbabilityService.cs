using FOSCBot.Core.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace FOSCBot.Core.Services;

public class ProbabilityService(IMemoryCache cache, IOptions<FosboOptions> options)
{
    private readonly int _tippingPoint = options.Value.TippingPoint;

    public bool GetResult(string key)
    {
        var callCount = cache.GetOrCreate(key, entry => 0);
        callCount++;

        cache.Set(key, callCount);
        var probability = Math.Min(1.0, (double)callCount / (2 * _tippingPoint));
        return new Random().NextDouble() < probability;
    }

    public void Reset(string key)
    {
        cache.Set(key, 0);
    }
}