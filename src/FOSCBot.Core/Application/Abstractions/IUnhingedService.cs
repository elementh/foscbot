namespace FOSCBot.Core.Application.Abstractions;

public interface IUnhingedService
{
    bool IsUnhinged(long chatId);
    void SetUnhinged(long chatId);
    string? GetPrompt(long chatId);
    void SetPrompt(long chatId, string prompt);
    void Clear(long chatId);
}

