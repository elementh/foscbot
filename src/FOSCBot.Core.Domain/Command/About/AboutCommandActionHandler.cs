using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Domain.Command.About;

public class AboutCommandActionHandler : ActionHandler<AboutCommandAction>
{
    public AboutCommandActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(AboutCommandAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), CoreResources.AboutText, ParseMode.Markdown, cancellationToken: cancellationToken);
            
        return Unit.Value;
    }
}