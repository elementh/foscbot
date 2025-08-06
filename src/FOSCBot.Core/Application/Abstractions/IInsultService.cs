namespace FOSCBot.Core.Application.Abstractions;

public interface IInsultService
{
    Task<string> GetInsult(CancellationToken cancellationToken = default);
}