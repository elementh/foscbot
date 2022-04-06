using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.HeyBro;

public class HeyBroMiscellaneousAction : MessageAction
{
    protected static readonly string StickerEmoji = "😚";
    protected static readonly string StickerPack = "foscupct";

    public override bool CanHandleCurrentContext()
    {
        return RandomProvider.GetThreadRandom().NextDouble() < 0.8d
               && ctx.Update.Message.Sticker?.Emoji == StickerEmoji
               && ctx.Update.Message.Sticker?.SetName == StickerPack;
    }
}