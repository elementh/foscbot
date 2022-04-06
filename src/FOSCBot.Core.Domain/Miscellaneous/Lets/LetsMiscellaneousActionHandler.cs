using FOSCBot.Common.Helper;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Miscellaneous.Lets;

public class LetsMiscellaneousActionHandler : ActionHandler<LetsMiscellaneousAction>
{
    public LetsMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(LetsMiscellaneousAction action, CancellationToken cancellationToken)
    {
        var stickerList = LetsGoHelper.LetsGoStickers;
        var randomSticker = stickerList[RandomProvider.GetThreadRandom().Next(0, stickerList.Length)];
            
        await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, "FUCKING", cancellationToken: cancellationToken);
        await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, "GO", cancellationToken: cancellationToken);
        await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, randomSticker, cancellationToken: cancellationToken);
            
        return Success();
    }
}