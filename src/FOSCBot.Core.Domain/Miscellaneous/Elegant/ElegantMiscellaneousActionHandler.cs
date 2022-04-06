using FOSCBot.Core.Domain.Resources;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Miscellaneous.Elegant;

public class ElegantMiscellaneousActionHandler : ActionHandler<ElegantMiscellaneousAction>
{
    private const string Quote = "Elegance is achieved when all that is superfluous has been discarded and the human being discovers simplicity and concentration: " +
                                 "the simpler and more sober the posture, the more beautiful it will be.";
        
    public ElegantMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(ElegantMiscellaneousAction action, CancellationToken cancellationToken)
    {
        await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.Elegant, caption: Quote, cancellationToken: cancellationToken);
        return Success();
    }
}