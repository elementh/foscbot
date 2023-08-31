using FOSCBot.Infrastructure.Contract.Client;
using FOSCBot.Infrastructure.Contract.Service;
using Microsoft.Extensions.Logging;

namespace FOSCBot.Infrastructure.Implementation.Service;

public class MemeService : IMemeService
{
    private readonly ILogger<MemeService> _logger;
    private readonly IMemeClient _memeClient;

    public MemeService(ILogger<MemeService> logger, IMemeClient memeClient)
    {
        _logger = logger;
        _memeClient = memeClient;
    }

    public async Task<Stream?> GenerateMeme(string text, CancellationToken cancellationToken)
    {
        try
        {
            var uri = await _memeClient.GenerateMeme(text, cancellationToken);

            if (uri is not null)
            {
                var done = await CheckUri(uri, 7, cancellationToken);
                
                if (done)
                {
                    return await _memeClient.Download(uri, cancellationToken);
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled error retrieving meme.");

            return default;
        }

        return default;
    }

    private async Task<bool> CheckUri(Uri uri, int times, CancellationToken cancellationToken)
    {
        while (times > 0)
        {
            var done = await _memeClient.CheckIfDone(uri, cancellationToken);

            if (done)
            {
                return done;
            }
            
            await Task.Delay(1250, cancellationToken);

            times -= 1;
        }

        return false;
    }
}