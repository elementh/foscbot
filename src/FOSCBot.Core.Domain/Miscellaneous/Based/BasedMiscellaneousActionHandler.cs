using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

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