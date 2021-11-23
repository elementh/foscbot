using FOSCBot.Common.Helper;
using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Interactivity.Ping
{
    public class PingInteractiveAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return ctx.IsBotMentioned() && ctx.IsBotPinged();
        }
    }
}