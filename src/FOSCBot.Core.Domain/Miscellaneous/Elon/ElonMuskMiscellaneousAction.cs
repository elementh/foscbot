using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Elon;

public class ElonMuskMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return (ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("elon musk") ?? false) ||
               (ctx.GetMessageOrDefault()?.Text?.ToLower().StartsWith("elon") ?? false) ||
               (ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("elon ") ?? false) ||
               (ctx.GetMessageOrDefault()?.Text?.ToLower().EndsWith(" elon") ?? false) ||
               (ctx.GetMessageOrDefault()?.Text?.ToLower().StartsWith("musk") ?? false) ||
               (ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("musk ") ?? false) ||
               (ctx.GetMessageOrDefault()?.Text?.ToLower().EndsWith(" musk") ?? false);
    }
}