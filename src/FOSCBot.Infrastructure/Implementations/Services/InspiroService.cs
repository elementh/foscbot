using FOSCBot.Core.Application.Abstractions;
using FOSCBot.Infrastructure.Contracts.Clients;
using Microsoft.Extensions.Logging;

namespace FOSCBot.Infrastructure.Implementations.Services;

public class InspiroService : IInspiroService
{
    private readonly ILogger<InspiroService> _logger;
    private readonly IInspiroClient _inspiroClient;

    public InspiroService(ILogger<InspiroService> logger, IInspiroClient inspiroClient)
    {
        _logger = logger;
        _inspiroClient = inspiroClient;
    }

    public async Task<string> GetInspiroImage(CancellationToken cancellationToken = default)
    {
        try
        {
            var image = await _inspiroClient.Get(cancellationToken);

            return image;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled error retrieving inspiro image.");

            return default;
        }
    }
}