using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Fallback.Default;

[ActionPriority(Navigator.Actions.Priority.Low)]
public class DefaultFallbackAction : MessageAction
{
    public DefaultFallbackAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return true;
    }
}