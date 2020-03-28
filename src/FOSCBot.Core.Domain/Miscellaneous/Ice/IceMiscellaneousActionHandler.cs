using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Ice
{
    public class IceMiscellaneousActionHandler : ActionHandler<IceMiscellaneousAction>
    {
        public IceMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(IceMiscellaneousAction request, CancellationToken cancellationToken)
        {
            await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), VideoLinks.Ice, cancellationToken: cancellationToken);

            return Unit.Value;
        }
    }
}