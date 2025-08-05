using Telegram.Bot.Types;

namespace FOSCBot.Core.Modules.SocialCredit.Application.Abstractions.Infrastructure;

public interface IMessageQueueService
{
    Task EnqueueMessageAsync(Message message, CancellationToken cancellationToken = default);
    Task<Message?> DequeueMessageAsync(CancellationToken cancellationToken = default);
}