using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Common.Helper;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Command.Want;

public class WantCommandActionHandler : ActionHandler<WantCommandAction>
{
    public WantCommandActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(WantCommandAction request, CancellationToken cancellationToken)
    {
        var randomSticker = Stickers[RandomProvider.GetThreadRandom().Next(0, Stickers.Length)];

        await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), randomSticker, cancellationToken: cancellationToken);

        return Unit.Value;
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