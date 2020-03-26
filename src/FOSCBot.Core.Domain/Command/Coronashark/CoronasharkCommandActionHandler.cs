using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;

namespace FOSCBot.Core.Domain.Command.Coronashark
{
    public class CoronasharkCommandActionHandler : ActionHandler<CoronasharkCommandAction>
    {
        public static string CoronasharkSticker = "CAACAgQAAxkBAAI4_l59L095Ep-xxos5f_7KBYkVlbu5AAKcBgACL9trAAF-dsaP9FZw_hgE";
        
        public CoronasharkCommandActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(CoronasharkCommandAction request, CancellationToken cancellationToken)
        {
            await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), CoronasharkSticker, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}
