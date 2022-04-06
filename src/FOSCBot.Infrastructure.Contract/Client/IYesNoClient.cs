using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Infrastructure.Contract.Model;

namespace FOSCBot.Infrastructure.Contract.Client;

public interface IYesNoClient
{
    Task<YesNoAnswerModel> GetAnswer(string type, CancellationToken cancellationToken = default);
}