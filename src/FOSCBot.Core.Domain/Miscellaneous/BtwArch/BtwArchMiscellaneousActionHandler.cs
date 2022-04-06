using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Domain.Miscellaneous.BtwArch;

public class BtwArchMiscellaneousActionHandler : ActionHandler<BtwArchMiscellaneousAction>
{
    public BtwArchMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(BtwArchMiscellaneousAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), "`Btw I run on Arch Linux.`", ParseMode.Markdown,
            replyToMessageId: request.MessageId, cancellationToken: cancellationToken);

        return Unit.Value;
    }
}