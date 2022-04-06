using System.Threading;
using System.Threading.Tasks;

namespace FOSCBot.Infrastructure.Contract.Service;

public interface IYesNoService
{
    Task<string> GetYesImage(CancellationToken cancellationToken);
    Task<string> GetNoImage(CancellationToken cancellationToken);
    Task<string> GetMaybeImage(CancellationToken cancellationToken);
}