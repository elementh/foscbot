using Navigator.Extensions.Store.Entities;

namespace FOSCBot.Core.Modules.SocialCredit.Domain.Entities;

public class MessageScore
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public User User { get; set; } = null!;
    public Chat Chat { get; set; } = null!;

    public string? Text { get; set; }
    public int MessageId { get; set; }
    public int Score { get; set; }
    public string? Reasoning { get; set; }
    public DateTime ProcessedAt { get; set; }
}