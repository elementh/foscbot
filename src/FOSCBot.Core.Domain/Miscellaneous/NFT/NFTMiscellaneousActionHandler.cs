using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.NFT
{
    public class NFTMiscellaneousActionHandler : ActionHandler<NFTMiscellaneousAction>
    {
        public NFTMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(NFTMiscellaneousAction request, CancellationToken cancellationToken)
        {
            await Ctx.Client.SendPhotoAsync(Ctx.GetTelegramChat(), CoreLinks.NFT, "tEnGo Un IpAd", cancellationToken: cancellationToken);
            return Unit.Value;
        }
    }
}