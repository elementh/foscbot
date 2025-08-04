using FOSCBot.Core.Module.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace FOSCBot.Core.Application.Services;

public class ProbabilityService
{
    private readonly IMemoryCache _cache;
    private readonly int _tippingPoint;
    private readonly UnhingedService _unhingedService;

    public ProbabilityService(IMemoryCache cache, IOptions<FosboOptions> options, UnhingedService unhingedService)
    {
        _cache = cache;
        _unhingedService = unhingedService;
        _tippingPoint = options.Value.TippingPoint;
    }

    public bool GetResult(long chatId)
    {
        var key = $"fallback.catchall.probabilities:{chatId}";

        var callCount = _cache.GetOrCreate(key, entry => 0);
        callCount++;

        _cache.Set(key, callCount);
        var probability = Math.Min(1.0, (double)callCount / (2 * GetTippingPoint(chatId)));
        return new Random().NextDouble() < probability;
    }

    public void Reset(long chatId)
    {
        var key = $"fallback.catchall.probabilities:{chatId}";

        _cache.Set(key, 0);
    }

    private int GetTippingPoint(long chatId)
    {
        return _unhingedService.IsUnhinged(chatId)
            ? 1
            : _tippingPoint;
    }
}