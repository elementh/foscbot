using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Cagaste;

public class CagasteMiscellaneousAction : MessageAction
{
    public CagasteMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return (Message.Text?.ToLower().Equals("cagaste") ?? false)
               || (Message.Text?.ToLower().Equals("kgaste") ?? false);
    }
}