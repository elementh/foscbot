namespace FOSCBot.Infrastructure.Contracts.Clients;

public interface IInspiroClient
{
    Task<string> Get(CancellationToken cancellationToken = default);
}