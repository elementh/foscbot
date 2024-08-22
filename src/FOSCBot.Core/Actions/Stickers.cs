using FOSCBot.Core.Helpers;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;

namespace FOSCBot.Core.Actions;

public static class Stickers
{
    public static void RegisterStickers(this BotActionCatalogFactory catalog)
    {
        catalog
            .OnText((string text) => text.Contains("bill gates", StringComparison.CurrentCultureIgnoreCase) ||
                                     text.Contains("microsoft", StringComparison.CurrentCultureIgnoreCase))
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

        catalog
            .OnText((string text) => text.Contains("blahaj", StringComparison.InvariantCultureIgnoreCase))
            .SendRandomStickerFrom([
                "CAACAgIAAxkBAAI5bF594zvXetBu_fXNlzGdlYQT_OLUAAKnDwAC6NbiEtMAAXmxqEjqNRgE",
                "CAACAgIAAxkBAAI5bV594zuW5LFY5lap1LzE3-1Ld831AAKoDwAC6NbiErX0KrBKBbo-GAQ",
                "CAACAgIAAxkBAAI5dF594z2bIU_bNYQpHg0SKYfWRyZJAAKvDwAC6NbiEha0sCe1JYCaGAQ",
                "CAACAgIAAxkBAAI5c1594z2iAAGUc_4z4itqbsTt86st_wACqw8AAujW4hK3-pMtZGAoUBgE",
                "CAACAgIAAxkBAAI5b1594zxyl3s0joh4YKujn0VrqHukAAKqDwAC6NbiEhvGxsrcJVXAGAQ",
                "CAACAgIAAxkBAAI5cl594z3xP0GdgNHrF_ceKQZuj9veAAKsDwAC6NbiEsBF-qdfxEwRGAQ",
                "CAACAgIAAxkBAAI5cF594zwlZoChfffIsnuHrJWqdcCjAAKuDwAC6NbiEnS1zuDiWOt3GAQ",
                "CAACAgIAAxkBAAI5dl594z4qx7tyTQuMhDESwKw7vzDAAAKxDwAC6NbiEp5U_nAi9RevGAQ",
                "CAACAgIAAxkBAAI5cV594zxpC3tzx5UILe-0IPk8bsE4AAKtDwAC6NbiErjcBpL_LqYUGAQ",
                "CAACAgIAAxkBAAI5d1594z7iO9OQWSX5tH8ibObyT9kBAAKyDwAC6NbiEofgG9qvAamUGAQ",
                "CAACAgIAAxkBAAI5dV594z6GQq79M5IQqlmyVlN764gdAAKwDwAC6NbiEmXy9GvRv6IMGAQ",
                "CAACAgIAAxkBAAI5bl594zusYQABRBEnY9BSrUAFFLXJ3wACqQ8AAujW4hLEc6S4DolrBhgE",
                "CAACAgIAAxkBAAI5hF595Fj0eLynt4F6cB-Y1ZpMq3NlAAKzDwAC6NbiEkc3sur5QD78GAQ",
                "CAACAgIAAxkBAAI5hV595FisbWIsHSlRGUEDuKD5ck6LAAK0DwAC6NbiEsEKIzk2z9sgGAQ",
                "CAACAgIAAxkBAAI5il595FqU3wtBMc_NOc-I61FdWOcjAAK4DwAC6NbiEpLohASvMx9tGAQ",
                "CAACAgIAAxkBAAI5iV595FnbYl0D2wq7jn7hYhSB0KfyAAK5DwAC6NbiEprTyTPl54pPGAQ",
                "CAACAgIAAxkBAAI5iF595FlThTN-p9rqkmbe-OSf2tIAA7oPAALo1uIS1Z4MceBsiQgYBA",
                "CAACAgIAAxkBAAI5jV595FuzjzftvX77_8NfBBhX4wPMAAK8DwAC6NbiEuome0j3MAXeGAQ",
                "CAACAgIAAxkBAAI5i1595FozOBbqurRPjZkz8xrYFLlTAAK3DwAC6NbiEiB5CLFVvu1AGAQ",
                "CAACAgIAAxkBAAI5kF595FuJlWES8C7odBM-fi6ltHhDAAK-DwAC6NbiEuD27wGVbvsrGAQ",
                "CAACAgIAAxkBAAI5hl595FiHxz3_XAABrNYAAV34Xb2L_j0AArUPAALo1uISfJZG5bVRL0kYBA",
                "CAACAgIAAxkBAAI5jF595FpBj5H1P1A53AdicBzpnq3IAAK7DwAC6NbiEoMqUtUVRc0SGAQ",
                "CAACAgIAAxkBAAI5j1595Fs9ZymnHEMPaylJeB3vVp1cAAK9DwAC6NbiEhqv9ZuydzrUGAQ",
                "CAACAgIAAxkBAAI5h1595FnW9ceEuWSDFI5ens7QCa6FAAK2DwAC6NbiEm3K86PRpSCMGAQ",
                "CAACAgIAAxkBAAI5nF595LEOEOJ1eRlCaYezmvJLN11kAAK_DwAC6NbiEp79tW2n3igbGAQ",
                "CAACAgIAAxkBAAI5nV595LEHx9awv6UzyfnSjLyh1R9uAALADwAC6NbiEv8vHdNLpSarGAQ",
                "CAACAgIAAxkBAAI5ol595LPCGvehuPDf-5bWnTBuTEgfAALEDwAC6NbiEnU-Wh_y6lYMGAQ",
                "CAACAgIAAxkBAAI5o1595LNo3SUlKX-bxeDkFuMnmjg-AALDDwAC6NbiEoKgg2XurKSlGAQ",
                "CAACAgIAAxkBAAI5pF595LN-QTgWl7S1RW3mgMcgogW7AALHDwAC6NbiErYIN3J2nQHAGAQ",
                "CAACAgIAAxkBAAI5p1595LTIu3DC3henBMoW9qOy_oDpAALJDwAC6NbiEuxpaM8d808JGAQ",
                "CAACAgIAAxkBAAI5oF595LLjA96eQlBhrZriZ6yCjD9gAALGDwAC6NbiEpWa1_GVUJBGGAQ",
                "CAACAgIAAxkBAAI5n1595LJkLfSb14boHN-Rp_AwJQp_AALCDwAC6NbiEivj2YQT54MRGAQ",
                "CAACAgIAAxkBAAI5nl595LHVIMSZSgenJc3vaUE7BxjmAALBDwAC6NbiEqXiIE3FUu-DGAQ",
                "CAACAgIAAxkBAAI5oV595LKzfv5_8M-D496e2rhftNT9AALFDwAC6NbiEvTBjYrm8N96GAQ",
                "CAACAgIAAxkBAAI5pl595LNpWhkzUXQ_yh3GT5Ikfm84AALIDwAC6NbiEmFiX-Lvy7vrGAQ",
                "CAACAgIAAxkBAAI5qV595LWjQdTVXtQpqawatroPk__7AALMDwAC6NbiEkoSlCN8O7KdGAQ",
                "CAACAgIAAxkBAAI5qF595LRyI-3cunhHD-CUZ1IM2Ku8AALKDwAC6NbiEvmAF_7XoCYTGAQ",
                "CAACAgIAAxkBAAI5ql595LWQ7XvK70ghpTZSrNMiTl7BAALLDwAC6NbiEppFzKtgf6JyGAQ"
            ])
            .WithName("Blahaj");
    }
}