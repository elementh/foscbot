using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Actions;
using Navigator.Context;

namespace FOSCBot.Core.Domain.Miscellaneous.Ree;

public class ReeMiscellaneousActionHandler : ActionHandler<ReeMiscellaneousAction>
{
    public ReeMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(ReeMiscellaneousAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.Ree, cancellationToken: cancellationToken);

        return Unit.Value;
    }
}