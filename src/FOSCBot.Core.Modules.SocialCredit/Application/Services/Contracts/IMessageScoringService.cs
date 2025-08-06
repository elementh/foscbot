using Telegram.Bot.Types;

namespace FOSCBot.Core.Modules.SocialCredit.Application.Services.Contracts;

public interface IMessageScoringService
{
    Task<(int Score, string Reasoning)> ScoreMessageAsync(Message message, CancellationToken cancellationToken = default);
}