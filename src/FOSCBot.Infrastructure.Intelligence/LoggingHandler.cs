using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FOSCBot.Infrastructure.Intelligence;

public class LoggingHandler : DelegatingHandler
{
    private readonly ILogger<LoggingHandler> _logger;
    private readonly IWebHostEnvironment _environment;

    public LoggingHandler(ILogger<LoggingHandler> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var requestId = Guid.NewGuid().ToString();
        var stopwatch = Stopwatch.StartNew();

        if (_environment.IsDevelopment())
        {
            // Log request
            var requestContent = await ReadContentAsync(request.Content);
            _logger.LogInformation("[{RequestId}] HTTP Request: {Method} {Uri}", requestId, request.Method, request.RequestUri);
            _logger.LogInformation("[{RequestId}] Request Headers: {Headers}", requestId, FormatHeaders(request.Headers));
            if (!string.IsNullOrEmpty(requestContent))
            {
                _logger.LogInformation("[{RequestId}] Request Body: {Body}", requestId, requestContent);
            }
        }

        // Send request
        var response = await base.SendAsync(request, cancellationToken);
        stopwatch.Stop();

        if (_environment.IsDevelopment())
        {
            // Log response
            var responseContent = await ReadContentAsync(response.Content);
            _logger.LogInformation("[{RequestId}] HTTP Response: {StatusCode} ({ElapsedMs}ms)", requestId, (int)response.StatusCode, stopwatch.ElapsedMilliseconds);
            _logger.LogInformation("[{RequestId}] Response Headers: {Headers}", requestId, FormatHeaders(response.Headers));
            if (!string.IsNullOrEmpty(responseContent))
            {
                _logger.LogInformation("[{RequestId}] Response Body: {Body}", requestId, responseContent);
            }
        }

        return response;
    }

    private static async Task<string> ReadContentAsync(HttpContent? content)
    {
        if (content == null) return string.Empty;
        
        var originalContent = await content.ReadAsStringAsync();
        return originalContent;
    }

    private static string FormatHeaders(IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers)
    {
        return string.Join(", ", headers.Select(h => $"{h.Key}: {string.Join(", ", h.Value)}"));
    }
}
