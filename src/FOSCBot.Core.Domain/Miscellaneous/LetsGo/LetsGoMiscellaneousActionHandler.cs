using FOSCBot.Common.Helper;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Miscellaneous.LetsGo;

public class LetsGoMiscellaneousActionHandler : ActionHandler<LetsGoMiscellaneousAction>
{
    public LetsGoMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(LetsGoMiscellaneousAction action, CancellationToken cancellationToken)
    {
        var stickerList = LetsGoHelper.LetsGoStickers;
        var randomSticker = stickerList[RandomProvider.GetThreadRandom().Next(0, stickerList.Length)];
            
        await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, randomSticker, cancellationToken: cancellationToken);
            
        return Success();
    }
}