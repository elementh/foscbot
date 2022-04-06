namespace FOSCBot.Infrastructure.Contract.Service;

public interface ILipsumService
{
    Task<string> GetBacon(string type = "all-meat", int sentences = 1, CancellationToken cancellationToken = default);
    Task<string> GetMetaphorSentence(int quantity = 1, CancellationToken cancellationToken = default);
    Task<string> GetMetaphorParagraph(int quantity = 1, CancellationToken cancellationToken = default);
}