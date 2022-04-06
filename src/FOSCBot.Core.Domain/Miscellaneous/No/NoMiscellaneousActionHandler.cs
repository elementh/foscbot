using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Miscellaneous.No;

public class NoMiscellaneousActionHandler : ActionHandler<NoMiscellaneousAction>
{
    public NoMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(NoMiscellaneousAction request, CancellationToken cancellationToken)
    {
        await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.Nope, cancellationToken: cancellationToken);

        return Success();
    }
}