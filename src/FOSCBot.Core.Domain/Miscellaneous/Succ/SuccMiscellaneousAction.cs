using Navigator;
using Navigator.Abstraction;
using Navigator.Extensions.Actions;
namespace FOSCBot.Core.Domain.Miscellaneous.Succ
{
    public class SuccMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return (ctx.GetMessageOrDefault()?.Text?.Equals("SUCC") ?? false) || (ctx.GetMessageOrDefault()?.Text?.Equals("SAC") ?? false);
        }
    }
}