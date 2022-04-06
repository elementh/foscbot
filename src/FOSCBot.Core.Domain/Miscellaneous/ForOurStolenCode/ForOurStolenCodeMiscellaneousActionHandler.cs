using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.ForOurStolenCode;

public class ForOurStolenCodeMiscellaneousActionHandler : ActionHandler<ForOurStolenCodeMiscellaneousAction>
{
    public ForOurStolenCodeMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(ForOurStolenCodeMiscellaneousAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.Orks, cancellationToken: cancellationToken);
            
        return Unit.Value;
    }
}