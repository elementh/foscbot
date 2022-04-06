using FOSCBot.Common.Helper;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Interactivity.BadBot;

public class BadBotInteractiveActionHandler : ActionHandler<BadBotInteractiveAction>
{
    public BadBotInteractiveActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(BadBotInteractiveAction action, CancellationToken cancellationToken)
    {
        var reactions = new List<string>
        {
            "Sowwry uwu",
            "Perdoooooooon",
            "... :(",
            "Habla con mis dueños para que me arreglen òwó",
            "Acho que no es mi culpa, me programaron así"
        };

        var response = reactions.GetRandomFromList();
            
        await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, response, cancellationToken: cancellationToken);
            
        return Success();
    }
}