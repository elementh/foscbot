using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Updates;

namespace FOSCBot.Core.Domain.Inline.Default;

public class DefaultInlineAction : InlineQueryAction
{
    public DefaultInlineAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return true;
    }
}