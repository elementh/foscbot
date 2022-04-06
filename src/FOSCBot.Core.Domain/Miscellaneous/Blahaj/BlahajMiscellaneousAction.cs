using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Blahaj;

public class BlahajMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return action.Message.Text?.ToLower().Contains("blahaj") ?? false;
    }
}