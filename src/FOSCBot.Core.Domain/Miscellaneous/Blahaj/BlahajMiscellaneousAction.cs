using Navigator;
using Navigator.Abstraction;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Blahaj
{
    public class BlahajMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("blahaj") ?? false;
        }
    }
}