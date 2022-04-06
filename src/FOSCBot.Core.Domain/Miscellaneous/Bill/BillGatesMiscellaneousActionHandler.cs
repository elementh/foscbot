using FOSCBot.Common.Helper;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Miscellaneous.Bill;

public class BillGatesMiscellaneousActionHandler : ActionHandler<BillGatesMiscellaneousAction>
{
        
    public BillGatesMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }
        
    public override async Task<Status> Handle(BillGatesMiscellaneousAction request, CancellationToken cancellationToken)
    {
        var randomSticker = Stickers[RandomProvider.GetThreadRandom().Next(0, Stickers.Length)];

        await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, randomSticker, cancellationToken: cancellationToken);
            
        return Success();
    }
        
    public static readonly string[] Stickers = {
        "CAACAgQAAxkBAAI7GV6AjQuBNOLeYNKM1SBiRWB7tnuiAAIVDgAC_wRTAAGOgxwVhlJSlBgE",
        "CAACAgIAAxkBAAI7G16AkmXDeUy_ub_bezQ1rCEoQJhaAAKEBwACYyviCdnd2EStFD0DGAQ",
        "CAACAgIAAxkBAAI7HV6AkohZo2o9brFBO2wXVaZw7WusAAKRBwACYyviCcQ168JPLJQxGAQ",
        "CAACAgIAAxkBAAI7H16AkqTrAQ7RuRodfkDnR22FgHi5AAKXBwACYyviCdpCtHXzTycRGAQ",
        "CAACAgIAAxkBAAI7IV6AkqefjL2tvmJFtmVg04eF3YLsAAKPBwACYyviCR9FAgcEOrD5GAQ",
        "CAACAgIAAxkBAAI7I16Akqs0MGoJKRvozewtn8rD-s-BAAKYBwACYyviCTG5W40KakR2GAQ",
        "CAACAgIAAxkBAAI7Jl6Akxqjt-lLYrVUo5I4NjgNaNigAAKDBwACYyviCX-4JBrvWaA3GAQ"
    };
}