namespace FOSCBot.Core.Application.Abstractions;

public interface ISilenceService
{
    void Silence(long chatId, TimeSpan duration);
    void Unsilence(long chatId);
    bool IsSilenced(long chatId);
}
