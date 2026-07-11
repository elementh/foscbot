namespace FOSCBot.Common.Persistence.Entities;

public class UserFallback
{
    public Guid Id { get; set; }
    public long TelegramUserExternalId { get; set; }
    public double Odds { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<UserFallbackSentence> Sentences { get; set; } = [];
}
