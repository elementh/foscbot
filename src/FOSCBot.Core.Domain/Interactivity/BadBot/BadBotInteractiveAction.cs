using FOSCBot.Common.Helper;
using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Interactivity.BadBot;

public class BadBotInteractiveAction : MessageAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        return ctx.IsBotQuotedOrMentioned() && ctx.IsBotBeingToldBadThings();
    }
}