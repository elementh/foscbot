using FOSCBot.Core.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Persistence.Context;

namespace FOSCBot.Persistence.Context;

public class FosboDbContext : NavigatorStoreDbContext, IFosboDbContext
{
    public FosboDbContext(DbContextOptions<NavigatorStoreDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}