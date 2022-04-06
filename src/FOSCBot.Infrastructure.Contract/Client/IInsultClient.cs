using System.Threading;
using System.Threading.Tasks;

namespace FOSCBot.Infrastructure.Contract.Client;

public interface IInsultClient
{
    Task<string> Get(CancellationToken cancellationToken = default);
}