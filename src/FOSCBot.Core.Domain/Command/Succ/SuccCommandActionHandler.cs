using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Command.Succ;

public class SuccCommandActionHandler : ActionHandler<SuccCommandAction>
{

    public SuccCommandActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(SuccCommandAction request, CancellationToken cancellationToken)
    {
        if (RandomProvider.GetThreadRandom().NextDouble() < 0.8d)
            await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.Succ, cancellationToken: cancellationToken);
        else
            await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.SuccWithTeeth, cancellationToken: cancellationToken);

        return Success();
    }
}