using System.Text.RegularExpressions;
using FOSCBot.Common.Helper;
using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.GoAhead
{
    public class GoAheadMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return RandomProvider.GetThreadRandom().NextDouble() <= 0.8d
                   && Regex.IsMatch(ctx.Update.Message.Text ?? string.Empty, @"[Gg][Oo]+[ ]+[Aa]+[Hh]+[Ee]+[Aa]+[Dd]+");
        }
    }
}