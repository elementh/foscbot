using FOSCBot.Common.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FOSCBot.Persistence.Configurations;

public class UserFallbackSentenceConfiguration : IEntityTypeConfiguration<UserFallbackSentence>
{
    public void Configure(EntityTypeBuilder<UserFallbackSentence> builder)
    {
        builder.HasKey(e => e.Id);
    }
}
