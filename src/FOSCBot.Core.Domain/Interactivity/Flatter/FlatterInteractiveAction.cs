using FOSCBot.Common.Helper;
using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Interactivity.Flatter
{
    public class FlatterInteractiveAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return ctx.IsBotQuotedOrMentioned() && ctx.IsBotFlattered();
        }
    }
}