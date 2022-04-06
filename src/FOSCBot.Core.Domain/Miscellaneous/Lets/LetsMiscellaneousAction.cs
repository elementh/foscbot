using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Lets;

public class LetsMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return (action.Message.Text?.Equals("LETS") ?? false)
               || (action.Message.Text?.Equals("LET'S") ?? false);
    }
}