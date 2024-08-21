using FOSCBot.Core.Old.Resources;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Old.Miscellaneous.Ree;

public class ReeMiscellaneousActionHandler : ActionHandler<ReeMiscellaneousAction>
{
    public ReeMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(ReeMiscellaneousAction action, CancellationToken cancellationToken)
    {
        await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.Ree, cancellationToken: cancellationToken);

        return Success();
    }
}