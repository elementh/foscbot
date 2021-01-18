using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Upct
{
    public class UpctMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            var message = ctx.GetMessageOrDefault()?.Text?.ToLower();
            return (message?.Contains("upct") ?? false) && !message.Contains("/upct");
        }
    }
}
