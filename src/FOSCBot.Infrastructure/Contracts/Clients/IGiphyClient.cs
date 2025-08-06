namespace FOSCBot.Infrastructure.Contracts.Clients;

public interface IGiphyClient
{
    Task<Uri?> Get(string text, CancellationToken cancellationToken = default);
}