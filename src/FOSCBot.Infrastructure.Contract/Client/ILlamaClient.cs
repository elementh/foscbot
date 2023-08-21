namespace FOSCBot.Infrastructure.Contract.Client;

public interface ILlamaClient
{
    Task<string?> GetChatCompletions(string chat, string? prompt);
}