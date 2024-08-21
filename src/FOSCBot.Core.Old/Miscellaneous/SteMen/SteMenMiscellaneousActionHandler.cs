using FOSCBot.Core.Old.Resources;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Old.Miscellaneous.SteMen;

public class SteMenMiscellaneousActionHandler : ActionHandler<SteMenMiscellaneousAction>
{
    public SteMenMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(SteMenMiscellaneousAction action, CancellationToken cancellationToken)
    {
        await NavigatorContext.GetTelegramClient().SendPhotoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.Stemen, cancellationToken: cancellationToken);
            
        return Success();
    }
}