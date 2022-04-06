using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Command.Succ;

public class SuccCommandActionHandler : ActionHandler<SuccCommandAction>
{

    public SuccCommandActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(SuccCommandAction request, CancellationToken cancellationToken)
    {
        if (RandomProvider.GetThreadRandom().NextDouble() < 0.8d)
            await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.Succ, cancellationToken: cancellationToken);
        else
            await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.SuccWithTeeth, cancellationToken: cancellationToken);

        return Unit.Value;
    }
}