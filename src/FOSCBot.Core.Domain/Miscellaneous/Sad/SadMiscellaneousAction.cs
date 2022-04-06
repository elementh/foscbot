using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Sad;

public class SadMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        if (RandomProvider.GetThreadRandom().NextDouble() < 0.6d)
        {
            return false;
        }
            
        return (action.Message.Text?.ToLower().Equals("sad") ?? false)
               ||(action.Message.Text?.ToLower().Contains(" sad ") ?? false)
               || action.Message.Sticker?.Emoji == "😔"
               || action.Message.Sticker?.Emoji == "😢"
               || action.Message.Sticker?.Emoji == "😞"
               || action.Message.Sticker?.Emoji == "😭";
    }
}