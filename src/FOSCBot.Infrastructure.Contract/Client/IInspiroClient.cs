namespace FOSCBot.Infrastructure.Contract.Client;

public interface IInspiroClient
{
    Task<string> Get(CancellationToken cancellationToken = default);
}