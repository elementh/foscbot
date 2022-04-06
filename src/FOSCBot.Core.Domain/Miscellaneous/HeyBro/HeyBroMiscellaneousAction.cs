using FOSCBot.Common.Helper;
using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.HeyBro;

public class HeyBroMiscellaneousAction : MessageAction
{
    protected static readonly string StickerEmoji = "😚";
    protected static readonly string StickerPack = "foscupct";

    public override bool CanHandle(INavigatorContext ctx)
    {
        return RandomProvider.GetThreadRandom().NextDouble() < 0.8d
               && ctx.Update.Message.Sticker?.Emoji == StickerEmoji
               && ctx.Update.Message.Sticker?.SetName == StickerPack;
    }
}