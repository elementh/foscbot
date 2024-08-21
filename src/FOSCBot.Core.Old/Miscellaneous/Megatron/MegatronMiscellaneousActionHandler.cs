using FOSCBot.Common.Helper;
using FOSCBot.Core.Old.Resources;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Old.Miscellaneous.Megatron;

public class MegatronMiscellaneousActionHandler : ActionHandler<MegatronMiscellaneousAction>
{
    public MegatronMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(MegatronMiscellaneousAction action, CancellationToken cancellationToken)
    {
        if (RandomProvider.GetThreadRandom().NextDouble() <= 0.5d)
            await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.MegatronCbtExperience, cancellationToken: cancellationToken);
        else
            await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.MegatronCbtImmediate, cancellationToken: cancellationToken);
        return Success();
    }
}