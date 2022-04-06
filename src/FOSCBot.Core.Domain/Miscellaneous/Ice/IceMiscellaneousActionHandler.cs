using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Ice;

public class IceMiscellaneousActionHandler : ActionHandler<IceMiscellaneousAction>
{
    public IceMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(IceMiscellaneousAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.Ice, cancellationToken: cancellationToken);

        return Unit.Value;
    }
}