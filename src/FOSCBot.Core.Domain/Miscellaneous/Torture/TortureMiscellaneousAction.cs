using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Torture;

public class TortureMiscellaneousAction : MessageAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        return (ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("cock and balls torture") ?? false) 
               || (ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("cock and ball torture") ?? false) 
               || (ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("cum blast me") ?? false)
               || (ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("cbt") ?? false);
    }
}