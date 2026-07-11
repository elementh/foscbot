using FOSCBot.Common.Persistence.Entities;

namespace FOSCBot.Core.Application.Abstractions;

public interface IUserMemoryService
{
    Task<UserMemory?> GetAsync(long telegramUserId);
    Task<UserMemory?> GetOrCreateAsync(long telegramUserId);
    Task SaveAsync(UserMemory memory);
    Task AccumulateMessageAsync(long chatId, long telegramUserId, string username, string text);
}
