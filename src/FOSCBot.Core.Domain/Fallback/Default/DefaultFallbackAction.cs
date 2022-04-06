using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Fallback.Default;

public class DefaultFallbackAction : MessageAction
{
    public new int Order = 1100;

    public override bool CanHandle(INavigatorContext ctx)
    {
        return true;
    }
}