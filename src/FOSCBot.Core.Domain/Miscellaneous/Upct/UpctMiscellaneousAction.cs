using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Upct;

public class UpctMiscellaneousAction : MessageAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        var message = ctx.GetMessageOrDefault()?.Text?.ToLower();
        return (message?.Contains("upct") ?? false) && !message.Contains("/upct");
    }
}