using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Interactivity.Ping;

public class PingInteractiveAction : MessageAction
{
    public PingInteractiveAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return NavigatorContextAccessor.NavigatorContext.IsBotQuotedOrMentioned() 
               && NavigatorContextAccessor.NavigatorContext.IsBotPinged();
    }
}