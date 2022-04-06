using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Command.Raniilove;

public class RaniiLoveCommandActionHandler : ActionHandler<RaniiloveCommandAction>
{
    public RaniiLoveCommandActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(RaniiloveCommandAction request, CancellationToken cancellationToken)
    {
        await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, RaniiSticker, cancellationToken: cancellationToken);

        return Success();
    }

    public static string RaniiSticker = "CAACAgEAAxkBAAMyXn0ejAABhNQUUOtuxi41w8zpW1kbAAKNAAM4DoIRRihUBMGXYkoYBA";
}