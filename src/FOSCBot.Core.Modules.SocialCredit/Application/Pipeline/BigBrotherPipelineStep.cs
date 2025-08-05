using FOSCBot.Core.Modules.SocialCredit.Application.Abstractions.Infrastructure;
using FOSCBot.Core.Modules.SocialCredit.Application.Services;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Modules.SocialCredit.Application.Pipeline;

public class BigBrotherPipelineStep : IActionResolutionPipelineStepAfter
{
    private readonly IMessageQueueService _messageQueueService;
    private readonly ILogger<BigBrotherPipelineStep> _logger;

    public BigBrotherPipelineStep(IMessageQueueService messageQueueService, ILogger<BigBrotherPipelineStep> logger)
    {
        _messageQueueService = messageQueueService;
        _logger = logger;
    }

    public async Task InvokeAsync(NavigatorActionResolutionContext context, PipelineStepHandlerDelegate next)
    {
        try
        {
            if (context.Update is { Type: UpdateType.Message, Message: not null } update)
            {
                var message = update.Message;

                if (message.From?.IsBot == true || message.Type == MessageType.Unknown)
                {
                    return;
                }

                if (message.Type != MessageType.Text || string.IsNullOrWhiteSpace(message.Text))
                {
                    return;
                }

                _logger.LogDebug("Enqueueing message {MessageId} from user {UserId} in chat {ChatId} for social credit processing",
                    message.MessageId, message.From?.Id, message.Chat.Id);

                await _messageQueueService.EnqueueMessageAsync(message);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to process update {UpdateId} in social credit pipeline", context.Update.Id);
        }
        finally
        {
            await next();
        }
    }
}