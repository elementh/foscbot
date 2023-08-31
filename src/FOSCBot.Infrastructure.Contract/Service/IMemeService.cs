namespace FOSCBot.Infrastructure.Contract.Service;

public interface IMemeService
{
    Task<Stream?> GenerateMeme(string text, CancellationToken cancellationToken);
}