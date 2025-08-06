using FOSCBot.Infrastructure.Contracts.Models;

namespace FOSCBot.Infrastructure.Contracts.Clients;

public interface IYesNoClient
{
    Task<YesNoAnswerModel> GetAnswer(string type, CancellationToken cancellationToken = default);
}