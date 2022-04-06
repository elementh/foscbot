using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Bueling;

public class BuelingMiscellaneousActionHandler : ActionHandler<BuelingMiscellaneousAction>
{
    public BuelingMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(BuelingMiscellaneousAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), "Did some carbon based life form just mention...", cancellationToken: cancellationToken);
        await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), "CAACAgQAAxkBAAJJpl6bSONlqhE0C21-0T9V9YHxfqPKAAKZBgACL9trAAHwqRcYUmB_gRgE", cancellationToken: cancellationToken);
            
        return Unit.Value;
    }
}