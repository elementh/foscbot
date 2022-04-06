using FOSCBot.Core.Domain.Resources;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Domain.Miscellaneous.HeyBro;

public class HeyBroMiscellaneousActionHandler : ActionHandler<HeyBroMiscellaneousAction>
{
    public HeyBroMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(HeyBroMiscellaneousAction action, CancellationToken cancellationToken)
    {
        var bytes = Convert.FromBase64String(CoreResources.HeyBroImage);
        await using (var stream = await new StreamContent(new MemoryStream(bytes)).ReadAsStreamAsync())
        {
            await NavigatorContext.GetTelegramClient().SendPhotoAsync(NavigatorContext.GetTelegramChat()!, new InputMedia(stream, "heybroniced.jpg"), 
                cancellationToken: cancellationToken);
        }

        return Success();
    }
}