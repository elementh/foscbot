using System.Text.RegularExpressions;
using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.GoAhead;

public class GoAheadMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return RandomProvider.GetThreadRandom().NextDouble() <= 0.8d
               && Regex.IsMatch(ctx.Update.Message.Text ?? string.Empty, @"[Gg][Oo]+[ ]+[Aa]+[Hh]+[Ee]+[Aa]+[Dd]+");
    }
}