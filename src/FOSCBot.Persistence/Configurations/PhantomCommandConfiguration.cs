using FOSCBot.Common.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FOSCBot.Persistence.Configurations;

public class PhantomCommandConfiguration : IEntityTypeConfiguration<PhantomCommand>
{
    public void Configure(EntityTypeBuilder<PhantomCommand> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Name);

        builder.HasMany(e => e.Chats)
            .WithOne(e => e.PhantomCommand)
            .HasForeignKey(e => e.PhantomCommandId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
