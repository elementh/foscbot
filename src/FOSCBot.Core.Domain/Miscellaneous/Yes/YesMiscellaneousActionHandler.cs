using MediatR;
using Navigator.Actions;
using Navigator.Context;

namespace FOSCBot.Core.Domain.Miscellaneous.Yes;

public class YesMiscellaneousActionHandler : ActionHandler<YesMiscellaneousAction>
{
    public YesMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(YesMiscellaneousAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), Yes, cancellationToken: cancellationToken);

        return Unit.Value;
    }
        
    public static readonly string Yes = "CAACAgQAAxkBAAI5HF59wcwDyRdwkEU3m_4CMMoz06CwAAKvAwACSy1sAAHbWFZ7iah6TRgE";
}