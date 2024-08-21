using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Old.Miscellaneous.HeyBro;

public class HeyBroMiscellaneousAction : MessageAction
{
    protected static readonly string StickerEmoji = "😚";
    protected static readonly string StickerPack = "foscupct";

    public HeyBroMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return RandomProvider.GetThreadRandom().NextDouble() < 0.8d
               && Message.Sticker?.Emoji == StickerEmoji
               && Message.Sticker?.SetName == StickerPack;
    }
}