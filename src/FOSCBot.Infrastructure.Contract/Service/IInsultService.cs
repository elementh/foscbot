namespace FOSCBot.Infrastructure.Contract.Service;

public interface IInsultService
{
    Task<string> GetInsult(CancellationToken cancellationToken = default);
}