namespace FOSCBot.Core.Application.Abstractions;

public interface IAdminAuthService
{
    string GenerateCode(long telegramUserId);
    Task<bool> VerifyCodeAsync(long telegramUserId, string code);
    Task<bool> IsAdminAsync(long telegramUserId);
}
