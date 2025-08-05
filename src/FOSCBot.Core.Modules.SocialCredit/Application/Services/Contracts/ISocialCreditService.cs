using FOSCBot.Core.Modules.SocialCredit.Domain.Entities;

namespace FOSCBot.Core.Modules.SocialCredit.Application.Services.Contracts;

public interface ISocialCreditService
{
    Task<Credit> GetCreditAsync(long telegramUserId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates credit for a user in a chat using Telegram external IDs
    /// </summary>
    Task<Credit> UpdateCreditAsync(long telegramUserId, int scoreChange, CancellationToken cancellationToken = default);

    /// <summary>
    /// Records a message score using Telegram external IDs
    /// </summary>
    Task<MessageScore> RecordMessageScoreAsync(long telegramUserId, long telegramChatId, int messageId, int score, 
        string? reasoning = null, string? messageText = null, CancellationToken cancellationToken = default);
}