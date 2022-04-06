using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.SteMen;

public class SteMenMiscellaneousAction : MessageAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        return ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("ste men") ?? false;
    }
}