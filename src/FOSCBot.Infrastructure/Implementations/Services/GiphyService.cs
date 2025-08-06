using FOSCBot.Core.Application.Abstractions;
using FOSCBot.Infrastructure.Contracts.Clients;
using Incremental.Common.Random;
using Microsoft.Extensions.Logging;

namespace FOSCBot.Infrastructure.Implementations.Services;

public class GiphyService : IGiphyService
{
    private readonly IGiphyClient _giphyClient;
    private readonly ILogger<GiphyService> _logger;

    public GiphyService(ILogger<GiphyService> logger, IGiphyClient giphyClient)
    {
        _logger = logger;
        _giphyClient = giphyClient;
    }

    public async Task<Uri?> Get(string text, CancellationToken cancellationToken = default)
    {
        try
        {
            var gifUrl = await _giphyClient.Get(text, cancellationToken);

            return gifUrl;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled error retrieving gif");

            return default;
        }
    }

    public async Task<Uri?> GetOneOf(string[] texts, CancellationToken cancellationToken = default)
    {
        if (texts.Length == 0) return default;

        var randomText = texts.Length switch
        {
            1 => texts[0],
            _ => texts[RandomProvider.GetThreadRandom()!.Next(0, texts.Length)]
        };

        return await Get(randomText, cancellationToken);
    }
}