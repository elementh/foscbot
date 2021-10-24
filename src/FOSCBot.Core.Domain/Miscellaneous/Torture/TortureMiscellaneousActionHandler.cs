 
using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Torture
{
    public class TortureMiscellaneousActionHandler : ActionHandler<TortureMiscellaneousAction>
    {
        public TortureMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(TortureMiscellaneousAction request, CancellationToken cancellationToken)
        {
            if (RandomProvider.GetThreadRandom().NextDouble() <= 0.5d)
                await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.CbtExplanation, cancellationToken: cancellationToken);
            else
            {
                await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), "And make it snappy", cancellationToken: cancellationToken);
                await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.Conke, cancellationToken: cancellationToken);
            }
            return Unit.Value;
        }
    }
}
