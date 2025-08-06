using FOSCBot.Core.Application.Abstractions;
using FOSCBot.Infrastructure.Contracts.Clients;
using Microsoft.Extensions.Logging;

namespace FOSCBot.Infrastructure.Implementations.Services;

public class InsultService : IInsultService
{
    private readonly ILogger<InsultService> _logger;
    private readonly IInsultClient _insultClient;

    public InsultService(ILogger<InsultService> logger, IInsultClient insultClient)
    {
        _logger = logger;
        _insultClient = insultClient;
    }

    public async Task<string> GetInsult(CancellationToken cancellationToken = default)
    {
        try
        {
            var image = await _insultClient.Get(cancellationToken);

            return image;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled error retrieving insult.");

            return default;
        }
    }
}