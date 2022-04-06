using FOSCBot.Core.Domain.Resources;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Domain.Miscellaneous.Nginx;

public class NginxMiscellaneousActionHandler : ActionHandler<NginxMiscellaneousAction>
{
    public NginxMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(NginxMiscellaneousAction action, CancellationToken cancellationToken)
    {
        var bytes = Convert.FromBase64String(CoreResources.NginxImage);
        await using (var stream = await new StreamContent(new MemoryStream(bytes)).ReadAsStreamAsync())
        {
            await NavigatorContext.GetTelegramClient().SendPhotoAsync(NavigatorContext.GetTelegramChat()!, new InputMedia(stream, "nginx.jpg"), 
                cancellationToken: cancellationToken);
        }

        return Success();
    }
}