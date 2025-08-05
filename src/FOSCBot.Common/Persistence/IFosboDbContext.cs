using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Entities;

namespace FOSCBot.Common.Persistence;

public interface IFosboDbContext
{
    // Navigator Store entities
    DbSet<User> Users { get; set; }
    DbSet<Chat> Chats { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}