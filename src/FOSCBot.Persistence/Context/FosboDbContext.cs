using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Persistence.Context;

namespace FOSCBot.Persistence.Context;

public class FosboDbContext : NavigatorStoreDbContext
{

    public FosboDbContext(DbContextOptions<FosboDbContext> options) : base(options)
    {
    }
}