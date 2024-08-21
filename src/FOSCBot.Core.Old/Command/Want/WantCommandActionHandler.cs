using FOSCBot.Common.Helper;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Old.Command.Want;

public class WantCommandActionHandler : ActionHandler<WantCommandAction>
{
    public WantCommandActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(WantCommandAction action, CancellationToken cancellationToken)
    {
        var randomSticker = Stickers[RandomProvider.GetThreadRandom().Next(0, Stickers.Length)];

        await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, randomSticker, cancellationToken: cancellationToken);

        return Success();
    }

    public static readonly string[] Stickers =
    {
        "CAACAgQAAxkBAAI5Yl59xpGz9uvLhFud46MfBOsOKAEZAAKRAAPXYpsOoD4HwY0npyEYBA",
        "CAACAgIAAxkBAAI5Y159xpQAAcud_5qj0XBWWtBYqzJ5OAACLVQAAp7OCwAB3ByKGvkbQr8YBA",
        "CAACAgIAAxkBAAI5Zl59xpcmSV8AAW8fzV2mOUkGCQeJCQACAwEAAladvQoC5dF4h-X6TxgE",
        "CAACAgQAAxkBAAI5aF59xp59Pnx0Y9_7iFLQwq56EP0jAAJ1FwAC_wRTAAGZhZMAARxrsr0YBA",
        "CAACAgEAAxkBAAI5al59xrCk3wAB13zrjcEKqtCJlnvpNwACDgcAAknjsAhFpPCKz57EtRgE"
    };
}