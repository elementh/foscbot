using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Torture;

public class TortureMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return (action.Message.Text?.ToLower().Contains("cock and balls torture") ?? false) 
               || (action.Message.Text?.ToLower().Contains("cock and ball torture") ?? false) 
               || (action.Message.Text?.ToLower().Contains("cum blast me") ?? false)
               || (action.Message.Text?.ToLower().Contains("cbt") ?? false);
    }
}