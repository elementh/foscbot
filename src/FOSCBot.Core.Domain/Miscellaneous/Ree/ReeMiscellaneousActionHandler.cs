using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Miscellaneous.Ree;

public class ReeMiscellaneousActionHandler : ActionHandler<ReeMiscellaneousAction>
{
    public ReeMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(ReeMiscellaneousAction request, CancellationToken cancellationToken)
    {
        await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.Ree, cancellationToken: cancellationToken);

        return Success();
    }
}