using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Nvidia;

public class NvidiaMiscellaneousActionHandler : ActionHandler<NvidiaMiscellaneousAction>
{
    public NvidiaMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(NvidiaMiscellaneousAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.Nvidia, cancellationToken: cancellationToken);

        return Unit.Value;
    }
}