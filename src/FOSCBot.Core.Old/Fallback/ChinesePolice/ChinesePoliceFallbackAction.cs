using FOSCBot.Common.Helper;
using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Extensions.Cooldown;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Old.Fallback.ChinesePolice;

[Cooldown(Seconds = 20 * 24 * 60 * 60)]
[ActionPriority(Navigator.Actions.Priority.Low)]
public class ChinesePoliceFallbackAction : MessageAction
{
    public string Word { get; protected set; }

    public ChinesePoliceFallbackAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {

    }

    public override bool CanHandleCurrentContext()
    {
        if (RandomProvider.GetThreadRandom().NextDouble() > 0.7)
        {
            return false;
        }

        return true;

    }
}