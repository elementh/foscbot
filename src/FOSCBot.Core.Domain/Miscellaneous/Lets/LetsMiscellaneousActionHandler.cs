using FOSCBot.Common.Helper;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Lets;

public class LetsMiscellaneousActionHandler : ActionHandler<LetsMiscellaneousAction>
{
    public LetsMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(LetsMiscellaneousAction request, CancellationToken cancellationToken)
    {
        var stickerList = LetsGoHelper.LetsGoStickers;
        var randomSticker = stickerList[RandomProvider.GetThreadRandom().Next(0, stickerList.Length)];
            
        await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), "FUCKING", cancellationToken: cancellationToken);
        await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), "GO", cancellationToken: cancellationToken);
        await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), randomSticker, cancellationToken: cancellationToken);
            
        return Unit.Value;
    }
}