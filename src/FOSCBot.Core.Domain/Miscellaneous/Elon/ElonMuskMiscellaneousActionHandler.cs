using FOSCBot.Common.Helper;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Miscellaneous.Elon;

public class ElonMuskMiscellaneousActionHandler : ActionHandler<ElonMuskMiscellaneousAction>
{
        
    public ElonMuskMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }
        
    public override async Task<Status> Handle(ElonMuskMiscellaneousAction request, CancellationToken cancellationToken)
    {
        var randomSticker = Stickers[RandomProvider.GetThreadRandom().Next(0, Stickers.Length)];

        await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, randomSticker, cancellationToken: cancellationToken);
            
        return Success();
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