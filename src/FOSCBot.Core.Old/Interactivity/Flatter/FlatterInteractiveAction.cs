using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Old.Interactivity.Flatter;

public class FlatterInteractiveAction : MessageAction
{
    public FlatterInteractiveAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return NavigatorContextAccessor.NavigatorContext.IsBotQuotedOrMentioned() 
               && NavigatorContextAccessor.NavigatorContext.IsBotFlattered();
    }
}