using Microsoft.Extensions.Diagnostics.HealthChecks;
using Python.Runtime;

namespace FOSCBot.Bot.Api.Health;

public class PythonHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        using (Py.GIL())
        {
            if (PythonEngine.IsInitialized)
            {
                return Task.FromResult(HealthCheckResult.Healthy("Python is enabled and configured."));
            }
        }

        return Task.FromResult(HealthCheckResult.Unhealthy("Python is not enabled."));
    }
}