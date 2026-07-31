namespace FOSCBot.Core.Application.Abstractions;

public interface IYappingService
{
    bool IsYapping(long chatId);
    void Toggle(long chatId, TimeSpan duration);
}
