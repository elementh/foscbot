using FOSCBot.Core.Modules.SocialCredit.Application.Abstractions.Infrastructure;
using FOSCBot.Core.Modules.SocialCredit.Application.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FOSCBot.Core.Modules.SocialCredit.Application.Services.Background;

public class MessageProcessingBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MessageProcessingBackgroundService> _logger;

    public MessageProcessingBackgroundService(IServiceProvider serviceProvider, ILogger<MessageProcessingBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Message processing background service started with rate limiting (5 messages/second)");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var queueService = scope.ServiceProvider.GetRequiredService<IMessageQueueService>();

                var message = await queueService.DequeueMessageAsync(stoppingToken);

                if (message != null)
                {
                    await ProcessMessageAsync(message, stoppingToken);
                }
                else
                {
                    // No messages available, wait a bit before checking again
                    await Task.Delay(100, stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message queue");
                await Task.Delay(1000, stoppingToken);
            }
        }

        _logger.LogInformation("Message processing background service stopped");
    }

    private async Task ProcessMessageAsync(Telegram.Bot.Types.Message message, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var scoringService = scope.ServiceProvider.GetRequiredService<IMessageScoringService>();
        var creditService = scope.ServiceProvider.GetRequiredService<ISocialCreditService>();
        
        try
        {
            if (message.From == null)
            {
                _logger.LogWarning("Message {MessageId} has no sender information", message.MessageId);
                return;
            }

            var userId = message.From.Id;
            var chatId = message.Chat.Id;

            _logger.LogDebug("Processing message {MessageId} from user {UserId} in chat {ChatId}",
                message.MessageId, userId, chatId);
            
            var (score, reasoning) = await scoringService.ScoreMessageAsync(message, cancellationToken);

            await creditService.RecordMessageScoreAsync(userId, chatId, message.MessageId, score, reasoning, message.Text, 
                cancellationToken);

            await creditService.UpdateCreditAsync(userId, score, cancellationToken);
                
            _logger.LogDebug("Processed message {MessageId}: score={Score}, reasoning={Reasoning}",
                message.MessageId, score, reasoning);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process message {MessageId}", message.MessageId);
        }
    }
}