using System;
using System.Threading;
using System.Threading.Tasks;

namespace FOSCBot.Infrastructure.Contract.Service
{
    public interface IGiphyService
    {
        Task<Uri?> Get(string text, CancellationToken cancellationToken = default);
    }
}