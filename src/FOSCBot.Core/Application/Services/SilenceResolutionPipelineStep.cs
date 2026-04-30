using FOSCBot.Core.Application.Abstractions;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Priorities;
using Navigator.Abstractions.Telegram;

namespace FOSCBot.Core.Application.Services;

[Priority(EPriority.High)]
public class SilenceResolutionPipelineStep(ISilenceService silenceService,
    ILogger<SilenceResolutionPipelineStep> logger) : IActionResolutionPipelineStepAfter
{
    public Task InvokeAsync(NavigatorActionResolutionContext context, PipelineStepHandlerDelegate next)
    {
        var update = context.UpdateContext.Update;
        var chat = update.GetChatOrDefault();

        if (chat is null || !silenceService.IsSilenced(chat.Id))
        {
            return next();
        }

        var text = update.Message?.Text?.Trim();
        var isSpeakCommand = text is not null &&
                             (text.Equals("/speak", StringComparison.OrdinalIgnoreCase) ||
                              text.StartsWith("/speak@", StringComparison.OrdinalIgnoreCase));

        if (isSpeakCommand)
        {
            for (var i = context.Actions.Count - 1; i >= 0; i--)
            {
                if (context.Actions[i].Information.Name == "Command.Speak") continue;

                context.Actions.RemoveAt(i);
            }

            return next();
        }

        if (context.Actions.Count > 0)
        {
            logger.LogDebug("Suppressing {ActionCount} actions for silenced chat {ChatId}", context.Actions.Count, chat.Id);
            context.Actions.Clear();
        }

        return next();
    }
}
