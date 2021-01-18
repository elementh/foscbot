using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Ipad
{
    public class IpadMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return ctx.Update.Message.Text?.ToLower().Contains(" ipad") ?? false;
        }
    }
}