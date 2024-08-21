using FOSCBot.Core.Old.Resources;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Old.Miscellaneous.Stonks;

public class StonksMiscellaneousActionHandler : ActionHandler<StonksMiscellaneousAction>
{
    public StonksMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(StonksMiscellaneousAction action, CancellationToken cancellationToken)
    {
        await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.Stonks, cancellationToken: cancellationToken);
            
        return Success();
    }
}