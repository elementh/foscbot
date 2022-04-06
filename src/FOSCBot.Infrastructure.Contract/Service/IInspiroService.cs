namespace FOSCBot.Infrastructure.Contract.Service;

public interface IInspiroService
{
    Task<string> GetInspiroImage(CancellationToken cancellationToken = default);
}