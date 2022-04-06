using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Succ;

public class SuccMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return (Message.Text?.Equals("SUCC") ?? false) || (Message.Text?.Equals("SAC") ?? false);
    }
}