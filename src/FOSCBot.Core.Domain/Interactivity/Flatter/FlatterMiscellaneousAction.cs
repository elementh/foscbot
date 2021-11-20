using System.Text.RegularExpressions;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Interactivity.Flatter
{
    public class FlatterMiscellaneousAction : MessageAction
    {
        private const int FoscBotUserId = 970438602;

        public override bool CanHandle(INavigatorContext ctx)
        {
            return ctx.GetMessageOrDefault()?.ReplyToMessage?.From?.Id == FoscBotUserId &&
                   ((ctx.GetMessageOrDefault()?.Text?.ToLower().Equals("si") ?? false) ||
                    (ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("acho") ?? false) ||
                    (ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("jajaja") ?? false) ||
                    (ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("gracias") ?? false) ||
                    Regex.IsMatch(ctx.GetMessageOrDefault()?.Text!, @"[Jj][Oo]+[Dd][Ee]+[Rr]+"));
        }
    }
}