using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Cagaste
{
    public class CagasteMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return (ctx.GetMessageOrDefault()?.Text?.ToLower().Equals("cagaste") ?? false)
                   || (ctx.GetMessageOrDefault()?.Text?.ToLower().Equals("kgaste") ?? false);
        }
    }
}