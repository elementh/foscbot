using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Domain.Miscellaneous.Ipad;

public class IpadMiscellaneousActionHandler : ActionHandler<IpadMiscellaneousAction>
{
    public IpadMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(IpadMiscellaneousAction action, CancellationToken cancellationToken)
    {
        if (RandomProvider.GetThreadRandom().NextDouble() >= 0.5)
        {
            await NavigatorContext.GetTelegramClient().SendPhotoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.Ipad, "tEnGo Un IpAd", cancellationToken: cancellationToken);
        }
        else
        {
            var bytes = Convert.FromBase64String(CoreResources.IpadAudio);
            await using var stream = await new StreamContent(new MemoryStream(bytes)).ReadAsStreamAsync();
                
            await NavigatorContext.GetTelegramClient().SendVoiceAsync(NavigatorContext.GetTelegramChat()!, new InputMedia(stream, "ipad"), duration: 5, cancellationToken: cancellationToken);
        }

        return Success();
    }
}