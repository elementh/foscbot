using Navigator.Extensions.Store.Entities;

namespace FOSCBot.Core.Modules.SocialCredit.Domain.Entities;

public class Credit
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public User User { get; set; } = null!;
    public int Score { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}