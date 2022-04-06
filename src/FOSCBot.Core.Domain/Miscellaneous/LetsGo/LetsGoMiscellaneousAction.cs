using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.LetsGo;

public class LetsGoMiscellaneousAction : MessageAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        return (ctx.GetMessageOrDefault()?.Text?.ToLower().StartsWith("let's fucking go") ?? false)
               || (ctx.GetMessageOrDefault()?.Text?.ToLower().StartsWith("lets fucking go") ?? false)
               || (ctx.GetMessageOrDefault()?.Text?.ToLower().StartsWith("let's fuckin go") ?? false)
               || (ctx.GetMessageOrDefault()?.Text?.ToLower().StartsWith("lets fuckin go") ?? false);
    }
}