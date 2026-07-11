namespace FOSCBot.Common.Persistence.Entities;

public class UserFallbackSentence
{
    public Guid Id { get; set; }
    public Guid UserFallbackId { get; set; }
    public UserFallback UserFallback { get; set; } = null!;
    public required string Text { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
