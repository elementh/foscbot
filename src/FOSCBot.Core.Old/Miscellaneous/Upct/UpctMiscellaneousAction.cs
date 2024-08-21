using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Old.Miscellaneous.Upct;

public class UpctMiscellaneousAction : MessageAction
{
    public UpctMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        var message = Message.Text?.ToLower();
        return (message?.Contains("upct") ?? false) && !message.Contains("/upct");
    }
}