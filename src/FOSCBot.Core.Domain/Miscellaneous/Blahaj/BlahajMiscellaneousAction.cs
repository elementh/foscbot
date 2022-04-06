using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Blahaj;

public class BlahajMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("blahaj") ?? false;
    }
}