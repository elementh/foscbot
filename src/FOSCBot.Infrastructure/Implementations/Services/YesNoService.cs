﻿using FOSCBot.Core.Application.Abstractions;
using FOSCBot.Infrastructure.Contracts.Clients;
using Microsoft.Extensions.Logging;

namespace FOSCBot.Infrastructure.Implementations.Services;

public class YesNoService : IYesNoService
{
    private readonly ILogger<YesNoService> _logger;
    private readonly IYesNoClient _yesNoClient;

    public YesNoService(ILogger<YesNoService> logger, IYesNoClient yesNoClient)
    {
        _logger = logger;
        _yesNoClient = yesNoClient;
    }

    public async Task<string> GetYesImage(CancellationToken cancellationToken)
    {
        try
        {
            var answer = await _yesNoClient.GetAnswer("yes", cancellationToken);

            return answer.Image;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled error retrieving answer.");

            return default;
        }
    }

    public async Task<string> GetNoImage(CancellationToken cancellationToken)
    {
        try
        {
            var answer = await _yesNoClient.GetAnswer("no", cancellationToken);

            return answer.Image;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled error retrieving answer.");

            return default;
        }
    }

    public async Task<string> GetMaybeImage(CancellationToken cancellationToken)
    {
        try
        {
            var answer = await _yesNoClient.GetAnswer("maybe", cancellationToken);

            return answer.Image;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled error retrieving answer.");

            return default;
        }
    }
}