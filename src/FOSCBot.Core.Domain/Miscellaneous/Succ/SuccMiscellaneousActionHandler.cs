using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Actions;
using Navigator.Context;

namespace FOSCBot.Core.Domain.Miscellaneous.Succ;

public class SuccMiscellaneousActionHandler : ActionHandler<SuccMiscellaneousAction>
{
    public SuccMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(SuccMiscellaneousAction request, CancellationToken cancellationToken)
    {
        if (RandomProvider.GetThreadRandom().NextDouble() < 0.8d)
            await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.Succ, cancellationToken: cancellationToken);
        else
            await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.SuccWithTeeth, cancellationToken: cancellationToken);
            
        return Unit.Value;
    }
}