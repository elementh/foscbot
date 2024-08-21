using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Old.Miscellaneous.Lets;

public class LetsMiscellaneousAction : MessageAction
{
    public LetsMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return (Message.Text?.Equals("LETS") ?? false)
               || (Message.Text?.Equals("LET'S") ?? false);
    }
}