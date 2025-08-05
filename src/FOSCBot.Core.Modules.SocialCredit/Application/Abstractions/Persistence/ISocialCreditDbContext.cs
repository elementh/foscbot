using FOSCBot.Common.Persistence;
using FOSCBot.Core.Modules.SocialCredit.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FOSCBot.Core.Modules.SocialCredit.Application.Abstractions.Persistence;

public interface ISocialCreditDbContext : IFosboDbContext
{
    DbSet<Credit> Credits { get; set; }
    DbSet<MessageScore> MessageScores { get; set; }
}