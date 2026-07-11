using FOSCBot.Common.Persistence;
using FOSCBot.Common.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Persistence.Context;

namespace FOSCBot.Persistence.Context;

public class FosboDbContext : NavigatorStoreDbContext, IFosboDbContext
{
    public DbSet<PhantomCommand> PhantomCommands { get; set; }
    public DbSet<PhantomCommandChat> PhantomCommandChats { get; set; }
    public DbSet<Master> Masters { get; set; }
    public DbSet<UserFallback> UserFallbacks { get; set; }
    public DbSet<UserFallbackSentence> UserFallbackSentences { get; set; }

    public FosboDbContext(DbContextOptions<FosboDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FosboDbContext).Assembly);
    }
}