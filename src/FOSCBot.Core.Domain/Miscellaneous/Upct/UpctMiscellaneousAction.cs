using FOSCBot.Common.Helper;
using Navigator;
using Navigator.Abstraction;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Upct
{
    public class UpctMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            var message = ctx.GetMessageOrDefault()?.Text?.ToLower();
            return message.Contains("upct") && !message.Contains("/upct");
        }
    }
}
