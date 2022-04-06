using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Actions;
using Navigator.Context;

namespace FOSCBot.Core.Domain.Miscellaneous.Stonks;

public class StonksMiscellaneousActionHandler : ActionHandler<StonksMiscellaneousAction>
{
    public StonksMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(StonksMiscellaneousAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.Stonks, cancellationToken: cancellationToken);
            
        return Unit.Value;
    }
}