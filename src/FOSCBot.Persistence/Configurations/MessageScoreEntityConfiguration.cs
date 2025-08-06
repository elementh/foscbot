using FOSCBot.Core.Modules.SocialCredit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FOSCBot.Persistence.Configurations;

public class MessageScoreEntityConfiguration : IEntityTypeConfiguration<MessageScore>
{
    public void Configure(EntityTypeBuilder<MessageScore> builder)
    {
        builder.HasKey(ms => ms.Id);

        builder.Property(ms => ms.Score)
            .IsRequired();

        builder.Property(ms => ms.ProcessedAt)
            .IsRequired();

        builder.Property(ms => ms.MessageId)
            .IsRequired();

        builder.HasOne(ms => ms.User)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ms => ms.Chat)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ms => ms.ProcessedAt);

        // Configure string properties with length constraints
        builder.Property(ms => ms.Reasoning)
            .HasMaxLength(1500)
            .IsUnicode();
    }
}