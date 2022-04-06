namespace FOSCBot.Infrastructure.Contract.Client;

public interface IBaconClient
{
    Task<string> Get(string type, int sentences, CancellationToken cancellationToken = default);
}