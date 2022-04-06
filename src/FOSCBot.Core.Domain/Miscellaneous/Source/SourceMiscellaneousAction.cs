using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Source;

public class SourceMiscellaneousAction : MessageAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        return (ctx.GetMessageOrDefault()?.Text?.ToLower().Equals("source?") ?? false)
               || (ctx.GetMessageOrDefault()?.Text?.ToLower().Equals("source") ?? false)
               || (ctx.GetMessageOrDefault()?.Text?.ToLower().Equals("sauce?") ?? false)
               || (ctx.GetMessageOrDefault()?.Text?.ToLower().Equals("sauce") ?? false)
               || (ctx.GetMessageOrDefault()?.Text?.ToLower().Equals("saus?") ?? false)
               || (ctx.GetMessageOrDefault()?.Text?.ToLower().Equals("saus") ?? false);
    }
}