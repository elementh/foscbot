using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Succ;

public class SuccMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return (ctx.GetMessageOrDefault()?.Text?.Equals("SUCC") ?? false) || (ctx.GetMessageOrDefault()?.Text?.Equals("SAC") ?? false);
    }
}