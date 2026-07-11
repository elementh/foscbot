using FOSCBot.Common.Persistence.Entities;

namespace FOSCBot.Core.Application.Abstractions;

public interface IUserFallbackService
{
    Task<UserFallback?> GetAsync(long telegramUserId);
}
