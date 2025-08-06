namespace FOSCBot.Infrastructure.Contracts.Clients;

public interface IInsultClient
{
    Task<string> Get(CancellationToken cancellationToken = default);
}