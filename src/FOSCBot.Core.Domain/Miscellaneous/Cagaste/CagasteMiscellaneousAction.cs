using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Cagaste;

public class CagasteMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return (action.Message.Text?.ToLower().Equals("cagaste") ?? false)
               || (action.Message.Text?.ToLower().Equals("kgaste") ?? false);
    }
}