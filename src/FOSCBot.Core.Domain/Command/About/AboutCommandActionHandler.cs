using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Domain.Command.About;

public class AboutCommandActionHandler : ActionHandler<AboutCommandAction>
{
    public AboutCommandActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(AboutCommandAction action, CancellationToken cancellationToken)
    {
        
        await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, 
            CoreResources.AboutText, ParseMode.Markdown, cancellationToken: cancellationToken);
            
        return Success();
    }
}