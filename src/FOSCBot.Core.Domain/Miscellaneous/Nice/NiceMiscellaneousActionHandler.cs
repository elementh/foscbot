using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Actions;
using Navigator.Context;

namespace FOSCBot.Core.Domain.Miscellaneous.Nice;

public class NiceMiscellaneousActionHandler : ActionHandler<NiceMiscellaneousAction>
{
    public NiceMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(NiceMiscellaneousAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.Nice, cancellationToken: cancellationToken);
            
        return Unit.Value;
    }
}