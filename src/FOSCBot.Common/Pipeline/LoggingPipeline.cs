using System.Diagnostics;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using Navigator.Actions;

namespace FOSCBot.Common.Pipeline;

public class LoggingPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingPipeline<TRequest, TResponse>> _logger;
    private readonly Watcher _watcher;

    public LoggingPipeline(ILogger<LoggingPipeline<TRequest, TResponse>> logger, Watcher watcher)
    {
        _logger = logger;
        _watcher = watcher;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestName = request?.GetType().FullName;
        var requestId = _watcher.Add(request);

        _logger.LogInformation("{RequestName}:{RequestId} started", 
            requestName, requestId);

        TResponse response;

        try
        {
            try
            {
                if (request.GetType().GetInterfaces().Any(i => i == typeof(IAction)))
                {
                    _logger.LogInformation("{RequestName}:{RequestId} has properties {@Properties}", 
                        requestName, requestId, JsonSerializer.Serialize(request));
                }
            }
            catch (NotSupportedException)
            {
                _logger.LogWarning("{RequestName}:{RequestId} properties could not be serialized", 
                    requestName, requestId);
            }

            response = await next();
        }
        finally
        {
            stopwatch.Stop();
                
            if (stopwatch.Elapsed.TotalSeconds >= 30)
            {
                _logger.LogWarning("{RequestName}:{RequestId} ended in {ExecutionTime}", 
                    requestName, requestId, $"{stopwatch.ElapsedMilliseconds}ms");
            }
            else
            {
                _logger.LogInformation("{RequestName}:{RequestId} ended in {ExecutionTime}",
                    requestName, requestId, $"{stopwatch.ElapsedMilliseconds}ms");
            }
        }

        return response;
    }
}