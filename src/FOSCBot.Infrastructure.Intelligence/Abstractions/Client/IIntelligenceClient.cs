using Microsoft.SemanticKernel.ChatCompletion;

namespace FOSCBot.Infrastructure.Intelligence.Abstractions.Client;

public interface IIntelligenceClient
{
    public Task<T?> GetChatMessageContentAsync<T>(ChatHistory history, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<string> GetChatMessageStreamAsync(ChatHistory history, CancellationToken cancellationToken = default);
}
