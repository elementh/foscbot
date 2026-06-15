using FOSCBot.Common.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FOSCBot.Persistence.Configurations;

public class MasterConfiguration : IEntityTypeConfiguration<Master>
{
    public void Configure(EntityTypeBuilder<Master> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.TelegramUserExternalId).IsUnique();
    }
}
