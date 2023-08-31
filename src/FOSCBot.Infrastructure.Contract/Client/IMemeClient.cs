namespace FOSCBot.Infrastructure.Contract.Client;

public interface IMemeClient
{
    Task<Uri?> GenerateMeme(string text, CancellationToken cancellationToken);

    Task<bool> CheckIfDone(Uri uri, CancellationToken cancellationToken);

    Task<Stream?> Download(Uri uri, CancellationToken cancellationToken);
}