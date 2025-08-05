using Microsoft.SemanticKernel.ChatCompletion;

namespace FOSCBot.Core.Modules.SocialCredit.Application.Abstractions.Infrastructure;

public interface IIntelligenceClient
{
    public Task<T?> GetChatMessageContentAsync<T>(ChatHistory history, CancellationToken cancellationToken = default);
}