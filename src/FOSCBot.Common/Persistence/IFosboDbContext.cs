using FOSCBot.Common.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Entities;

namespace FOSCBot.Common.Persistence;

public interface IFosboDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Chat> Chats { get; set; }

    DbSet<PhantomCommand> PhantomCommands { get; set; }
    DbSet<PhantomCommandChat> PhantomCommandChats { get; set; }
    DbSet<Master> Masters { get; set; }
    DbSet<UserFallback> UserFallbacks { get; set; }
    DbSet<UserFallbackSentence> UserFallbackSentences { get; set; }
    DbSet<UserMemory> UserMemories { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}