using MediatR;
using Navigator.Actions;
using Navigator.Context;

namespace FOSCBot.Core.Domain.Command.Raniilove;

public class RaniiLoveCommandActionHandler : ActionHandler<RaniiloveCommandAction>
{
    public RaniiLoveCommandActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(RaniiloveCommandAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), RaniiSticker, cancellationToken: cancellationToken);

        return Unit.Value;
    }

    public static string RaniiSticker = "CAACAgEAAxkBAAMyXn0ejAABhNQUUOtuxi41w8zpW1kbAAKNAAM4DoIRRihUBMGXYkoYBA";
}