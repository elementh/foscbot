using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Sad
{
    public class SadMiscellaneousActionHandler : ActionHandler<SadMiscellaneousAction>
    {
        public SadMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(SadMiscellaneousAction request, CancellationToken cancellationToken)
        {
            await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), SadCrstian, cancellationToken: cancellationToken);

            return Unit.Value;
        }
        
        public static readonly string SadCrstian = "CAACAgQAAxkBAAI5DF59uqkJYnqzc5LcnEC_bdp0rerIAAJsAwACmOejAAG_qYNUT_L_exgE";
    }
}