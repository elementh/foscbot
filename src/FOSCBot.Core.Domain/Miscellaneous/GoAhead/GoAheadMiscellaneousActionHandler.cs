using FOSCBot.Core.Domain.Resources;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Miscellaneous.GoAhead;

public class GoAheadMiscellaneousActionHandler : ActionHandler<GoAheadMiscellaneousAction>
{
    public GoAheadMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(GoAheadMiscellaneousAction action, CancellationToken cancellationToken)
    {
        await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.GoAhead, 
            cancellationToken: cancellationToken, caption: "SSSSSSSSSSSUCK YOUR OWN COCKKKKK");
        return Success();
    }
}