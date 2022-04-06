using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Torture;

public class TortureMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return (Message.Text?.ToLower().Contains("cock and balls torture") ?? false) 
               || (Message.Text?.ToLower().Contains("cock and ball torture") ?? false) 
               || (Message.Text?.ToLower().Contains("cum blast me") ?? false)
               || (Message.Text?.ToLower().Contains("cbt") ?? false);
    }
}