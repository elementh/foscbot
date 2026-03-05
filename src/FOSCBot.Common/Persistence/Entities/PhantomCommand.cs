namespace FOSCBot.Common.Persistence.Entities;

public class PhantomCommand
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Personality { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<PhantomCommandChat> Chats { get; set; } = [];
}
