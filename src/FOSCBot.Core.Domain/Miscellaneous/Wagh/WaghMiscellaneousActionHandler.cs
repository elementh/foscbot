using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Actions;
using Navigator.Context;

namespace FOSCBot.Core.Domain.Miscellaneous.Wagh;

public class WaghMiscellaneousActionHandler : ActionHandler<WaghMiscellaneousAction>
{
    public WaghMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(WaghMiscellaneousAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.Orks, cancellationToken: cancellationToken);
            
        return Unit.Value;
    }
}