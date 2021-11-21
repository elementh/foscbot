using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Common.Helper;
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
            if (RandomProvider.GetThreadRandom().NextDouble() > 0.5d)
            {
                await Ctx.Client.SendPhotoAsync(Ctx.GetTelegramChat(), CoreLinks.NFT, cancellationToken: cancellationToken);
            }
            else
            {
                await Ctx.Client.SendPhotoAsync(Ctx.GetTelegramChat(), CoreLinks.NFToad, cancellationToken: cancellationToken);
            }

            return Unit.Value;
        }
    }
}