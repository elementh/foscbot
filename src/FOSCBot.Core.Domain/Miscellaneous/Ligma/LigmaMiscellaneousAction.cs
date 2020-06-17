using Navigator.Abstraction;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Ligma
{
    public class LigmaMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return ctx.Update.Message.Text?.ToLower().Contains(" so sad") ?? false;
        }
    }
}
