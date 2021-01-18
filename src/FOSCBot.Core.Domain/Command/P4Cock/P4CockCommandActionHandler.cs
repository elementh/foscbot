using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Command.P4Cock
{
    public class P4CockCommandActionHandler : ActionHandler<P4CockCommandAction>
    {
        public P4CockCommandActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(P4CockCommandAction request, CancellationToken cancellationToken)
        {
            await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), P4Sticker, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    
        public static string P4Sticker = "CAACAgQAAxkBAAMvXn0csAE-VH1a5YlL_C3y_uvmyhoAAk4DAAIv22sAAQYHFmm8oYuhGAQ";
    }
}