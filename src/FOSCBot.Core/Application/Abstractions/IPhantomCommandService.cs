using FOSCBot.Common.Persistence.Entities;

namespace FOSCBot.Core.Application.Abstractions;

public interface IPhantomCommandService
{
    Task<PhantomCommand?> GetCommandAsync(string name, long chatExternalId);
    Task<PhantomCommand> SaveCommandAsync(string name, string description, string personality, long chatExternalId);
    Task DeleteAllAsync();
}
