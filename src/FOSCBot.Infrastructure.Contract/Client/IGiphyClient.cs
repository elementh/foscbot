namespace FOSCBot.Infrastructure.Contract.Client;

public interface IGiphyClient
{
    Task<Uri?> Get(string text, CancellationToken cancellationToken = default);
}