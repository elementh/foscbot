namespace FOSCBot.Infrastructure.Contract.Service;

public interface ILlamaService
{
    Task<string?> GetResponse(string[] chats, string? prompt);
}