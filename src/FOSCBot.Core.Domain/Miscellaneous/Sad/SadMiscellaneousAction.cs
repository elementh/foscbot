using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Sad;

public class SadMiscellaneousAction : MessageAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        if (RandomProvider.GetThreadRandom().NextDouble() < 0.6d)
        {
            return false;
        }
            
        return (ctx.GetMessageOrDefault()?.Text?.ToLower().Equals("sad") ?? false)
               ||(ctx.GetMessageOrDefault()?.Text?.ToLower().Contains(" sad ") ?? false)
               || ctx.GetMessageOrDefault()?.Sticker?.Emoji == "😔"
               || ctx.GetMessageOrDefault()?.Sticker?.Emoji == "😢"
               || ctx.GetMessageOrDefault()?.Sticker?.Emoji == "😞"
               || ctx.GetMessageOrDefault()?.Sticker?.Emoji == "😭";
    }
}