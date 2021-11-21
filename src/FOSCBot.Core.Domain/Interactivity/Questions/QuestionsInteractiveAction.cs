using FOSCBot.Common.Helper;
using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Interactivity.Questions
{
    public class QuestionsInteractiveAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return ctx.IsBotMentioned() && !ctx.IsBotPinged();
        }
    }
}