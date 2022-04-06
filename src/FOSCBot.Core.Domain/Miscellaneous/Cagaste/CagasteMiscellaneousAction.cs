using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Cagaste;

public class CagasteMiscellaneousAction : MessageAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        return (ctx.GetMessageOrDefault()?.Text?.ToLower().Equals("cagaste") ?? false)
               || (ctx.GetMessageOrDefault()?.Text?.ToLower().Equals("kgaste") ?? false);
    }
}