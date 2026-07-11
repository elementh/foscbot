using FOSCBot.Common.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FOSCBot.Persistence.Configurations;

public class UserFallbackConfiguration : IEntityTypeConfiguration<UserFallback>
{
    public void Configure(EntityTypeBuilder<UserFallback> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.TelegramUserExternalId).IsUnique();

        builder.HasMany(e => e.Sentences)
            .WithOne(e => e.UserFallback)
            .HasForeignKey(e => e.UserFallbackId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
