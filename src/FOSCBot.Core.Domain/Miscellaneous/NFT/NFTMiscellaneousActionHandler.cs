using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Actions;
using Navigator.Context;

namespace FOSCBot.Core.Domain.Miscellaneous.NFT;

public class NFTMiscellaneousActionHandler : ActionHandler<NFTMiscellaneousAction>
{
    public NFTMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(NFTMiscellaneousAction request, CancellationToken cancellationToken)
    {
        var nft = new List<string>
        {
            CoreLinks.NFT,
            CoreLinks.NFToad,
            CoreLinks.NFTractor,
            CoreLinks.NFTattoo,
            CoreLinks.NFTu
        }.GetRandomFromList();
            
        await Ctx.Client.SendPhotoAsync(Ctx.GetTelegramChat(), nft, cancellationToken: cancellationToken);

        return Unit.Value;
    }
}