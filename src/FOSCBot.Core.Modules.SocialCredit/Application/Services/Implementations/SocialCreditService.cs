using FOSCBot.Core.Modules.SocialCredit.Application.Abstractions.Persistence;
using FOSCBot.Core.Modules.SocialCredit.Application.Services.Contracts;
using FOSCBot.Core.Modules.SocialCredit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FOSCBot.Core.Modules.SocialCredit.Application.Services.Implementations;

public class SocialCreditService : ISocialCreditService
{
    private readonly ISocialCreditDbContext _dbContext;
    private readonly ILogger<SocialCreditService> _logger;

    public SocialCreditService(ISocialCreditDbContext dbContext, ILogger<SocialCreditService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Credit> GetCreditAsync(long telegramUserId, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.ExternalId == telegramUserId, cancellationToken)
                   ?? throw new ArgumentNullException();

        var credit = await _dbContext.Credits
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.User.Id == user.Id, cancellationToken);

        if (credit == null)
        {
            credit = new Credit
            {
                User = user,
                Score = 1000,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _dbContext.Credits.Add(credit);
            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Created new credit record for user {TelegramUserId}", telegramUserId);
        }

        return credit;
    }
    
    public async Task<Credit> UpdateCreditAsync(long telegramUserId, int scoreChange, CancellationToken cancellationToken = default)
    {
        var credit = await GetCreditAsync(telegramUserId, cancellationToken);

        credit.Score += scoreChange;
        credit.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return credit;
    }

    public async Task<MessageScore> RecordMessageScoreAsync(long telegramUserId, long telegramChatId, int messageId,
        int score, string? reasoning = null, string? messageText = null, CancellationToken cancellationToken = default)
    {
        // Find or create Navigator entities
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.ExternalId == telegramUserId, cancellationToken)
            ?? throw new ArgumentNullException();
        var chat = await _dbContext.Chats.FirstOrDefaultAsync(c => c.ExternalId == telegramChatId, cancellationToken)
            ?? throw new ArgumentNullException();

        var messageScore = new MessageScore()
        {
            User = user,
            Chat = chat,
            Text = messageText,
            MessageId = messageId,
            Score = score,
            Reasoning = reasoning,
            ProcessedAt = DateTime.UtcNow
        };

        _dbContext.MessageScores.Add(messageScore);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogDebug("Recorded message score for user {TelegramUserId} in chat {TelegramChatId}, message {MessageId}: {Score}",
            telegramUserId, telegramChatId, messageId, score);

        return messageScore;
    }
}