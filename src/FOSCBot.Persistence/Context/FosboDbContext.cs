using FOSCBot.Core.Modules.SocialCredit.Application.Abstractions.Persistence;
using FOSCBot.Core.Modules.SocialCredit.Domain.Entities;
using FOSCBot.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Persistence.Context;

namespace FOSCBot.Persistence.Context;

public class FosboDbContext : NavigatorStoreDbContext, ISocialCreditDbContext
{
    public DbSet<Credit> Credits { get; set; } = null!;
    public DbSet<MessageScore> MessageScores { get; set; } = null!;

    public FosboDbContext(DbContextOptions<FosboDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply entity configurations
        modelBuilder.ApplyConfiguration(new CreditEntityConfiguration());
        modelBuilder.ApplyConfiguration(new MessageScoreEntityConfiguration());
    }
}