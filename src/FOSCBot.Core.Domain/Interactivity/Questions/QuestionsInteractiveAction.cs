using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Interactivity.Questions;

public class QuestionsInteractiveAction : MessageAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        return ctx.IsBotMentioned() && 
               !ctx.IsBotPinged() && 
               !ctx.IsBotFlattered() && 
               !ctx.IsBotBeingToldBadThings();
    }
}