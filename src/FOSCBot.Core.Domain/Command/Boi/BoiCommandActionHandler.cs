using FOSCBot.Common.Helper;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Command.Boi;

public class BoiCommandActionHandler : ActionHandler<BoiCommandAction>
{
    public BoiCommandActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(BoiCommandAction action, CancellationToken cancellationToken)
    {
        var randomSticker = Stickers[RandomProvider.GetThreadRandom().Next(0, Stickers.Length)];

        await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!,
            randomSticker, cancellationToken: cancellationToken);

        return Success();
    }

    public static readonly string[] Stickers =
    {
        "CAACAgQAAxkBAAI5Hl59wgU-EjcPoQJXOk81dz15rU6vAAIXAAMN0oYC8U1SU9k9QFQYBA",
        "CAACAgQAAxkBAAI5H159wgZdNShe0hTs65NLdinvP9v5AAIOAAMN0oYCfWiupsMYNyUYBA",
        "CAACAgQAAxkBAAI5Il59wgpjoo8hIuSmS7HRs44rneMRAAIeAAMN0oYCa2VzcOI0p0sYBA",
    };
}