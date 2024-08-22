using FOSCBot.Core.Helpers;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;

namespace FOSCBot.Core.Actions;

public static class Stickers
{
    public static void RegisterStickers(this BotActionCatalogFactory catalog)
    {
        catalog
            .OnText((string text) => text.Contains("bill gates", StringComparison.CurrentCultureIgnoreCase) || text.Contains("microsoft"))
            .SendRandomStickerFrom([
                "CAACAgQAAxkBAAI7GV6AjQuBNOLeYNKM1SBiRWB7tnuiAAIVDgAC_wRTAAGOgxwVhlJSlBgE",
                "CAACAgIAAxkBAAI7G16AkmXDeUy_ub_bezQ1rCEoQJhaAAKEBwACYyviCdnd2EStFD0DGAQ",
                "CAACAgIAAxkBAAI7HV6AkohZo2o9brFBO2wXVaZw7WusAAKRBwACYyviCcQ168JPLJQxGAQ",
                "CAACAgIAAxkBAAI7H16AkqTrAQ7RuRodfkDnR22FgHi5AAKXBwACYyviCdpCtHXzTycRGAQ",
                "CAACAgIAAxkBAAI7IV6AkqefjL2tvmJFtmVg04eF3YLsAAKPBwACYyviCR9FAgcEOrD5GAQ",
                "CAACAgIAAxkBAAI7I16Akqs0MGoJKRvozewtn8rD-s-BAAKYBwACYyviCTG5W40KakR2GAQ",
                "CAACAgIAAxkBAAI7Jl6Akxqjt-lLYrVUo5I4NjgNaNigAAKDBwACYyviCX-4JBrvWaA3GAQ"
            ])
            .WithName("Bill Gates");
    }
}