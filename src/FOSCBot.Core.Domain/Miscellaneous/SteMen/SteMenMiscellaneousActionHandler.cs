using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Actions;
using Navigator.Context;

namespace FOSCBot.Core.Domain.Miscellaneous.SteMen;

public class SteMenMiscellaneousActionHandler : ActionHandler<SteMenMiscellaneousAction>
{
    public SteMenMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(SteMenMiscellaneousAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendPhotoAsync(Ctx.GetTelegramChat(), CoreLinks.Stemen, cancellationToken: cancellationToken);
            
        return Unit.Value;
    }
}