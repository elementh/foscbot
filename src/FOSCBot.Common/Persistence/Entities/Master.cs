namespace FOSCBot.Common.Persistence.Entities;

public class Master
{
    public Guid Id { get; set; }
    public long TelegramUserExternalId { get; set; }
    public DateTimeOffset AuthenticatedAt { get; set; }
}
