using System;
using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Infrastructure.Contract.Client;
using FOSCBot.Infrastructure.Contract.Service;
using Microsoft.Extensions.Logging;

namespace FOSCBot.Infrastructure.Implementation.Service
{
    public class GiphyService : IGiphyService
    {
        private readonly ILogger<GiphyService> _logger;
        private readonly IGiphyClient _giphyClient;

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
    }
}