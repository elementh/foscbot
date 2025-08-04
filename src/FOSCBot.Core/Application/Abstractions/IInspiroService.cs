namespace FOSCBot.Core.Application.Abstractions;

public interface IInspiroService
{
    Task<string> GetInspiroImage(CancellationToken cancellationToken = default);
}