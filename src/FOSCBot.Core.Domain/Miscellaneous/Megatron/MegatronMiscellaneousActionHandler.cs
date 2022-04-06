using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Megatron;

public class MegatronMiscellaneousActionHandler : ActionHandler<MegatronMiscellaneousAction>
{
    public MegatronMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(MegatronMiscellaneousAction request, CancellationToken cancellationToken)
    {
        if (RandomProvider.GetThreadRandom().NextDouble() <= 0.5d)
            await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.MegatronCbtExperience, cancellationToken: cancellationToken);
        else
            await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.MegatronCbtImmediate, cancellationToken: cancellationToken);
        return Unit.Value;
    }
}