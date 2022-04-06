namespace FOSCBot.Infrastructure.Contract.Client;

public interface IMetaphorClient
{
    Task<string> GetSentence(int quantity, CancellationToken cancellationToken = default);
    Task<string> GetParagraph(int quantity, CancellationToken cancellationToken = default);
}