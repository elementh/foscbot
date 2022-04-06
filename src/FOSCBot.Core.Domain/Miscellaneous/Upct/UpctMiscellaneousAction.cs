using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Upct;

public class UpctMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        var message = action.Message.Text?.ToLower();
        return (message?.Contains("upct") ?? false) && !message.Contains("/upct");
    }
}