using FOSCBot.Common.Helper;
using FOSCBot.Core.Old.Resources;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Old.Miscellaneous.Succ;

public class SuccMiscellaneousActionHandler : ActionHandler<SuccMiscellaneousAction>
{
    public SuccMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(SuccMiscellaneousAction action, CancellationToken cancellationToken)
    {
        if (RandomProvider.GetThreadRandom().NextDouble() < 0.8d)
            await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.Succ, cancellationToken: cancellationToken);
        else
            await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.SuccWithTeeth, cancellationToken: cancellationToken);
            
        return Success();
    }
}