using FOSCBot.Core.Domain.Resources;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Domain.Command.Start;

public class StartCommandActionHandler : ActionHandler<StartCommandAction>
{
    public StartCommandActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(StartCommandAction action, CancellationToken cancellationToken)
    {
        await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, CoreResources.StartText, ParseMode.Markdown, cancellationToken: cancellationToken);
            
        return Success();
    }
}