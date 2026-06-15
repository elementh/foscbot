using FOSCBot.Common.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FOSCBot.Persistence.Configurations;

public class PhantomCommandChatConfiguration : IEntityTypeConfiguration<PhantomCommandChat>
{
    public void Configure(EntityTypeBuilder<PhantomCommandChat> builder)
    {
        builder.HasKey(e => new { e.PhantomCommandId, e.ChatExternalId });
    }
}
