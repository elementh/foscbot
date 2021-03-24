using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Miscellaneous.Bill;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Elon
{
    public class ElonMuskMiscellaneousActionHandler : ActionHandler<ElonMuskMiscellaneousAction>
    {
        
        public ElonMuskMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }
        
        public override async Task<Unit> Handle(ElonMuskMiscellaneousAction request, CancellationToken cancellationToken)
        {
            var randomSticker = Stickers[RandomProvider.GetThreadRandom().Next(0, Stickers.Length)];

            await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), randomSticker, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
        
        public static readonly string[] Stickers = {
            "CAACAgIAAxkBAAECGX9gWyite1lkgqD944zLdk31Cn_MbQACmggAAnlc4gmF6N5K1J_nix4E",
            "CAACAgIAAxkBAAECGYFgWyjJmDQFzVqaqbVq51qNgq-iYAACgggAAnlc4gndXhh_OFD8Rx4E",
            "CAACAgIAAxkBAAECGYFgWyjJmDQFzVqaqbVq51qNgq-iYAACgggAAnlc4gndXhh_OFD8Rx4E",
            "CAACAgIAAxkBAAECGYFgWyjJmDQFzVqaqbVq51qNgq-iYAACgggAAnlc4gndXhh_OFD8Rx4E",
            "CAACAgIAAxkBAAECGYtgWyj4FPWvacn18y11asl4Qq8rZgAC7wgAAnlc4gnztBdi0FWsRh4E",
            "CAACAgIAAxkBAAECGY1gWyj6c-QMcGzNPWtfiGaPZE0WcwACkggAAnlc4glg2uUwtwJdvR4E"
        };
    }
}