using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Ree;

public class ReeMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return action.Message.Text?.Contains("REE") ?? false;
    }
}