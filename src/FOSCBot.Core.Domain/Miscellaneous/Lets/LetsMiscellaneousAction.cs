using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Lets
{
    public class LetsMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return (ctx.GetMessageOrDefault()?.Text?.Equals("LETS") ?? false)
                   || (ctx.GetMessageOrDefault()?.Text?.Equals("LET'S") ?? false);
        }
    }
}