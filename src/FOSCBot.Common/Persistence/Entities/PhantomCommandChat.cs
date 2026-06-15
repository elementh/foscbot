namespace FOSCBot.Common.Persistence.Entities;

public class PhantomCommandChat
{
    public Guid PhantomCommandId { get; set; }
    public PhantomCommand PhantomCommand { get; set; } = null!;

    public long ChatExternalId { get; set; }
}
