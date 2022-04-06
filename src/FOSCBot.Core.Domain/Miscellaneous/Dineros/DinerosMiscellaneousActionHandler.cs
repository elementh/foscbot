using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Actions;
using Navigator.Context;

namespace FOSCBot.Core.Domain.Miscellaneous.Dineros;

public class DinerosMiscellaneousActionHandler : ActionHandler<DinerosMiscellaneousAction>
{
    public DinerosMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(DinerosMiscellaneousAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.Dineros, cancellationToken: cancellationToken);

        return Unit.Value;
    }
}