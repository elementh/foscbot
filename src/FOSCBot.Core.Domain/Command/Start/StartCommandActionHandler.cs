using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Domain.Command.Start;

public class StartCommandActionHandler : ActionHandler<StartCommandAction>
{
    public StartCommandActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(StartCommandAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), CoreResources.StartText, ParseMode.Markdown, cancellationToken: cancellationToken);
            
        return Unit.Value;
    }
}