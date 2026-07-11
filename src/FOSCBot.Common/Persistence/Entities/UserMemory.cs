namespace FOSCBot.Common.Persistence.Entities;

public class UserMemory
{
    public Guid Id { get; set; }
    public long TelegramUserExternalId { get; set; }
    public required string Content { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
