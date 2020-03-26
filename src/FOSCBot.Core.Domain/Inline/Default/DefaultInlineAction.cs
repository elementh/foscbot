using Navigator.Abstraction;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Inline.Default
{
    public class DefaultInlineAction : InlineQueryAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return true;
        }
    }
}