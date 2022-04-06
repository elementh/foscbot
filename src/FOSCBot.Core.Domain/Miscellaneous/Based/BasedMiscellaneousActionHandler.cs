using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Actions;
using Navigator.Context;

namespace FOSCBot.Core.Domain.Miscellaneous.Based;

public class BasedMiscellaneousActionHandler : ActionHandler<BasedMiscellaneousAction>
{
    public BasedMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(BasedMiscellaneousAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.BasedDepartment, cancellationToken: cancellationToken);
        return Unit.Value;
    }
}