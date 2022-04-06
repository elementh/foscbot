using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;
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