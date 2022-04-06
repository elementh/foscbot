using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Actions;
using Navigator.Context;

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