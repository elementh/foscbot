 
using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Torture
{
    public class TortureMiscellaneousActionHandler : ActionHandler<TortureMiscellaneousAction>
    {
        public TortureMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(TortureMiscellaneousAction request, CancellationToken cancellationToken)
        {
            await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), "And make it snappy", cancellationToken: cancellationToken);
            await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.Conke, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}
