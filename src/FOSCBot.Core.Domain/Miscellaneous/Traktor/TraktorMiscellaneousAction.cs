using System.Text.RegularExpressions;
using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Traktor;

public class TraktorMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return (RandomProvider.GetThreadRandom().NextDouble() <= 0.2d &&
                (Message.Text?.ToLower().Equals("blyat") ?? false))
               || (Message.Text?.ToLower().Equals("traktor") ?? false)
               || (Message.Text?.ToLower().Equals("блядь") ?? false)
               || (Message.Text?.ToLower().Equals("трактор") ?? false)
               || Regex.IsMatch(Message.Text ?? string.Empty, @"[Bb][Ll][Yy][Aa]+[Tt]+")
               || Regex.IsMatch(Message.Text ?? string.Empty, @"[Tt][Rr][Aa]+[KkCc][Tt][Oo]+[Rr]+");
    }
}