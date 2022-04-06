using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Miscellaneous.Based;

public class BasedMiscellaneousActionHandler : ActionHandler<BasedMiscellaneousAction>
{
    public BasedMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(BasedMiscellaneousAction request, CancellationToken cancellationToken)
    {
        await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.BasedDepartment, cancellationToken: cancellationToken);
        return Success();
    }
}