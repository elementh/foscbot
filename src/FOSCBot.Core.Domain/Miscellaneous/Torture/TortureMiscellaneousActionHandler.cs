using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Torture;

public class TortureMiscellaneousActionHandler : ActionHandler<TortureMiscellaneousAction>
{
    public TortureMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(TortureMiscellaneousAction request, CancellationToken cancellationToken)
    {
        var choice = RandomProvider.GetThreadRandom().Next(0, 4);
        switch (choice)
        {
            case 0:
                await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.CbtExplanation, cancellationToken: cancellationToken);
                break;
            case 1:
                await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), "And make it snappy", cancellationToken: cancellationToken);
                await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.Conke, cancellationToken: cancellationToken);
                break;
            case 2:
                await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.MegatronCbtImmediate, cancellationToken: cancellationToken);
                break;
            case 3:
                await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.MegatronCbtExperience, cancellationToken: cancellationToken);
                break;
        }
            
        return Unit.Value;
    }
}