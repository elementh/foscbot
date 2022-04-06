using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Traktor;

public class TraktorMiscellaneousActionHandler : ActionHandler<TraktorMiscellaneousAction>
{
    public TraktorMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(TraktorMiscellaneousAction request, CancellationToken cancellationToken)
    {
        if (RandomProvider.GetThreadRandom().NextDouble() <= 0.2d)
            await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.BuenoFlipao, cancellationToken: cancellationToken);
        else
            await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.Traktor, cancellationToken: cancellationToken);

        return Unit.Value;
    }
}