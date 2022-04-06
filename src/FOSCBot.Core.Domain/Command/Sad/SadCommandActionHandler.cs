using MediatR;
using Navigator.Actions;
using Navigator.Context;

namespace FOSCBot.Core.Domain.Command.Sad;

public class SadCommandActionHandler : ActionHandler<SadCommandAction>
{
    public SadCommandActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(SadCommandAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), SadCrstian, cancellationToken: cancellationToken);

        return Unit.Value;
    }
        
    public static readonly string SadCrstian = "CAACAgQAAxkBAAI5DF59uqkJYnqzc5LcnEC_bdp0rerIAAJsAwACmOejAAG_qYNUT_L_exgE";
}