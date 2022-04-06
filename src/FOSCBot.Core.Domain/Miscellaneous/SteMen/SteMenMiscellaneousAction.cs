using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.SteMen;

public class SteMenMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return Message.Text?.ToLower().Contains("ste men") ?? false;
    }
}