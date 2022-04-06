using FOSCBot.Infrastructure.Contract.Client;
using FOSCBot.Infrastructure.Contract.Service;
using Microsoft.Extensions.Logging;

namespace FOSCBot.Infrastructure.Implementation.Service;

public class LipsumService : ILipsumService
{
    private readonly ILogger<LipsumService> _logger;
    private readonly IBaconClient _baconClient;
    private readonly IMetaphorClient _metaphorClient;

    public LipsumService(ILogger<LipsumService> logger, IBaconClient baconClient, IMetaphorClient metaphorClient)
    {
        _logger = logger;
        _baconClient = baconClient;
        _metaphorClient = metaphorClient;
    }

    public async Task<string> GetBacon(string type = "all-meat", int sentences = 1, CancellationToken cancellationToken = default)
    {
        try
        {
            var bacon = await _baconClient.Get(type, sentences, cancellationToken);

            return bacon;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled error retrieving bacon.");

            return default;
        }
    }

    public async Task<string> GetMetaphorSentence(int quantity = 1, CancellationToken cancellationToken = default)
    {
        try
        {
            var sentence = await _metaphorClient.GetSentence(quantity, cancellationToken);

            return sentence;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled error retrieving metaphor.");

            return default;
        }
    }

    public async Task<string> GetMetaphorParagraph(int quantity = 1, CancellationToken cancellationToken = default)
    {
        try
        {
            var sentence = await _metaphorClient.GetSentence(quantity, cancellationToken);

            return sentence;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled error retrieving metaphor.");

            return default;
        }
    }
}