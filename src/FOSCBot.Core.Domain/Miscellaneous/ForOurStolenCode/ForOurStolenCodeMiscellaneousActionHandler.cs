using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.ForOurStolenCode
{
    public class ForOurStolenCodeMiscellaneousActionHandler : ActionHandler<ForOurStolenCodeMiscellaneousAction>
    {
        public ForOurStolenCodeMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(ForOurStolenCodeMiscellaneousAction request, CancellationToken cancellationToken)
        {
            await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), VideoLinks.Orks, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}