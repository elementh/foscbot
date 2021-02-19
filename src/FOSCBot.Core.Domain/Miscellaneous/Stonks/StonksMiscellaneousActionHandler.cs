using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Domain.Miscellaneous.Stonks
{
    public class StonksMiscellaneousActionHandler : ActionHandler<StonksMiscellaneousAction>
    {
        public StonksMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(StonksMiscellaneousAction request, CancellationToken cancellationToken)
        {
            await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.Stonks, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}