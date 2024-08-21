using System.Text.RegularExpressions;
using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Old.Miscellaneous.GoAhead;

public class GoAheadMiscellaneousAction : MessageAction
{
    public GoAheadMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return RandomProvider.GetThreadRandom().NextDouble() <= 0.8d
               && Regex.IsMatch(Message.Text ?? string.Empty, @"[Gg][Oo]+[ ]+[Aa]+[Hh]+[Ee]+[Aa]+[Dd]+");
    }
}