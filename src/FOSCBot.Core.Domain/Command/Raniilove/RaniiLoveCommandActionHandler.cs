using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;

namespace FOSCBot.Core.Domain.Command.Raniilove
{
    public class RaniiLoveCommandActionHandler : ActionHandler<RaniiloveCommandAction>
    {
        public static string RaniiSticker = "CAACAgEAAxkBAAMyXn0ejAABhNQUUOtuxi41w8zpW1kbAAKNAAM4DoIRRihUBMGXYkoYBA";

        public RaniiLoveCommandActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(RaniiloveCommandAction request, CancellationToken cancellationToken)
        {
            await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), RaniiSticker, cancellationToken: cancellationToken);

            return Unit.Value;
        }
    }
}