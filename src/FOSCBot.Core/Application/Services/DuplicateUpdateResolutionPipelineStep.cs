using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Priorities;

namespace FOSCBot.Core.Application.Services;

/// <summary>
///     Suppresses updates whose ID has already been resolved recently. Telegram redelivers
///     an update whenever it does not receive a timely 2xx ack (slow processing, pod restart,
///     network blip), and every redelivery would otherwise trigger a brand-new reply.
/// </summary>
[Priority(EPriority.Highest)]
public class DuplicateUpdateResolutionPipelineStep(IMemoryCache cache,
    ILogger<DuplicateUpdateResolutionPipelineStep> logger) : IActionResolutionPipelineStepAfter
{
    private static readonly TimeSpan RetentionWindow = TimeSpan.FromHours(24);

    public Task InvokeAsync(NavigatorActionResolutionContext context, PipelineStepHandlerDelegate next)
    {
        var updateId = context.UpdateContext.Update.Id;
        var key = $"navigator.update.seen:{updateId}";

        if (cache.TryGetValue(key, out _))
        {
            logger.LogWarning("Suppressing redelivered update {UpdateId} with {ActionCount} resolved actions",
                updateId, context.Actions.Count);
            context.Actions.Clear();

            return next();
        }

        cache.Set(key, true, RetentionWindow);

        return next();
    }
}
