using Navigator;
using Navigator.Abstraction;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Ree
{
    public class ReeMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return ctx.GetMessageOrDefault()?.Text?.Contains("REE") ?? false;
        }
    }
}
