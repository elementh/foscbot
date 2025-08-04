namespace FOSCBot.Core.Application.Abstractions;

public interface IGiphyService
{
    Task<Uri?> Get(string text, CancellationToken cancellationToken = default);

    Task<Uri?> GetOneOf(string[] texts, CancellationToken cancellationToken = default);
}