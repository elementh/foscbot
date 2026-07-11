using FOSCBot.Common.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FOSCBot.Persistence.Configurations;

public class UserMemoryConfiguration : IEntityTypeConfiguration<UserMemory>
{
    public void Configure(EntityTypeBuilder<UserMemory> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.TelegramUserExternalId).IsUnique();
        builder.Property(e => e.Content).HasMaxLength(1024).IsRequired();
    }
}
