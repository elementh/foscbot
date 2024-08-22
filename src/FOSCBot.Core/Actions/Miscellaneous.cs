using FOSCBot.Core.Helpers;
using FOSCBot.Core.Resources;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Navigator.Client;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Actions;

public static class Miscellaneous
{
    public static void RegisterMiscellaneous(this BotActionCatalogFactory catalog)
    {
        catalog
            .OnText((string text) => text.ToLower().Equals("based") || text.Equals("BASED"),
                async (INavigatorClient client, Chat chat) =>
                {
                    const string based = "https://raw.githubusercontent.com/elementh/foscbot/master/assets/based_department.mp4";
                    await client.SendVideoAsync(chat, based);
                })
            .WithChances(0.2)
            .WithName("Mischellaneous.Based");

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
            .WithName("B$llGates");

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

        catalog
            .OnText((string text) => text.Contains("arch", StringComparison.CurrentCultureIgnoreCase),
                async (INavigatorClient client, Chat chat, Message message) =>
                {
                    await client.SendTextMessageAsync(chat, "`Btw I run on Arch Linux.`", parseMode: ParseMode.Markdown,
                        replyParameters: message);
                })
            .WithChances(0.4)
            .WithName("Miscellaneous.BtwArch");
        
        catalog
            .OnText((string text) =>
                text.ToLower().Contains("elon musk") ||
                text.ToLower().StartsWith("elon") ||
                text.ToLower().Contains("elon ") ||
                text.ToLower().EndsWith(" elon") ||
                text.ToLower().StartsWith("musk") ||
                text.ToLower().Contains("musk ") ||
                text.ToLower().EndsWith(" musk")
            )
            .SendRandomStickerFrom([
                "CAACAgIAAxkBAAECGX9gWyite1lkgqD944zLdk31Cn_MbQACmggAAnlc4gmF6N5K1J_nix4E",
                "CAACAgIAAxkBAAECGYFgWyjJmDQFzVqaqbVq51qNgq-iYAACgggAAnlc4gndXhh_OFD8Rx4E",
                "CAACAgIAAxkBAAECGYFgWyjJmDQFzVqaqbVq51qNgq-iYAACgggAAnlc4gndXhh_OFD8Rx4E",
                "CAACAgIAAxkBAAECGYFgWyjJmDQFzVqaqbVq51qNgq-iYAACgggAAnlc4gndXhh_OFD8Rx4E",
                "CAACAgIAAxkBAAECGYtgWyj4FPWvacn18y11asl4Qq8rZgAC7wgAAnlc4gnztBdi0FWsRh4E",
                "CAACAgIAAxkBAAECGY1gWyj6c-QMcGzNPWtfiGaPZE0WcwACkggAAnlc4glg2uUwtwJdvR4E"
            ])
            .WithName("Miscellaneous.FelonMask");

        catalog
            .OnSticker((Sticker sticker) => sticker.SetName?.Equals("foscupct") is not false && sticker.Emoji?.Equals("😚") is true,
                async (INavigatorClient client, Chat chat) =>
                {
                    var bytes = Convert.FromBase64String(CoreResources.HeyBroImage);

                    await using var stream = await new StreamContent(new MemoryStream(bytes)).ReadAsStreamAsync();

                    await client.SendPhotoAsync(chat, new InputFileStream(stream, "heybroniced.jpg"));
                })
            .WithChances(0.5)
            .WithName("HeyBro");
        
        catalog
            .OnText((string text) =>
                text.ToLower().Contains("je ") ||
                text.ToLower().Contains(" je") ||
                text.ToLower().Contains(" je ") ||
                text.ToLower().Contains("jeje") ||
                text.ToLower().Equals("je") ||
                text.ToLower().Contains("je je")
            )
            .SendRandomStickerFrom([
                "CAACAgIAAxkBAAI5Dl59vPOH6MA6Uzua49AHRz-q5mMUAAIKAQACMNSdEVZUV2nGbrlvGAQ",
                "CAACAgQAAxkBAAI5EF59vUKLQ46GEgbuzhY0O5r3HyauAAJKAQACqCEhBntEKK5RNh4XGAQ",
                "CAACAgIAAxkBAAI5El59vYqk6ywiJOKdXzXNe2gsPL2gAAL2AwACierlB263K9ogJ_bwGAQ",
                "CAACAgIAAxkBAAI5FF59vZFmQWYDsTaj4X9GJl9bPAbEAAJBAQAC-YQfHIsWbGjJcnqnGAQ",
                "CAACAgQAAxkBAAI5Fl59vb9993hlyxnbQ_VEZlEMqzymAAI7AgACMo1bAAEaE0PNwutkzBgE",
                "CAACAgIAAxkBAAI5GF59vfIG0bug-aIj8txxEBNNiNUXAAIQCAACGELuCOAfnJHe30ZuGAQ",
                "CAACAgIAAxkBAAI5Gl59vfg4AefyKXIXUAMOdoCs6gNAAALNBwACGELuCPlfWYiQZaQiGAQ"
            ])
            .WithName("Miscellaneous.Jeje");

        catalog
            .OnText((string text) => text.Contains("uwu", StringComparison.CurrentCultureIgnoreCase))
            .SendRandomStickerFrom([
                "CAACAgEAAxkBAAI5JF59w6XM_AcpKByOoe1DtXyJuAr0AAL4AgACzcclBQ7jFynAjnWwGAQ",
                "CAACAgEAAxkBAAI5JV59w6Vh9gyqRgRr0l0OWEtDLfT8AAL5AgACzcclBeHXi8c4JWp_GAQ",
                "CAACAgEAAxkBAAI5Jl59w6ZP94FPDlRGHkZOHCjduqDTAAL6AgACzcclBVRLfoZ5hPHJGAQ",
                "CAACAgEAAxkBAAI5K159w6dKWVAdIhgC5LBnVpqCsb2AAAIEAwACzcclBbf0eAdlZt5RGAQ",
                "CAACAgEAAxkBAAI5KF59w6YChoJ_RPRv1iSKOuwkilshAAIBAwACzcclBU_zIjPWHIMjGAQ",
                "CAACAgEAAxkBAAI5Kl59w6dnWCFLuEAxsywnFePrepevAAIiAwACzcclBfSPg197ugNkGAQ",
                "CAACAgEAAxkBAAI5J159w6buPfY7zD3NftMEB1AAAeS6YwADAwACzcclBTtAZlst9z6MGAQ",
                "CAACAgEAAxkBAAI5KV59w6cxHS1kuD4leFV2xFrk_4KBAAICAwACzcclBZnYLaPmqi5MGAQ",
                "CAACAgEAAxkBAAI5NF59w-iSDd7hrnYKOxMCXg8anOFHAAIFAwACzcclBX_KTiXzl-z5GAQ",
                "CAACAgEAAxkBAAI5NV59w-gHM75fhv_f20Qpl2_0GL2aAAIHAwACzcclBbd8te3xicWBGAQ",
                "CAACAgEAAxkBAAI5O159w-rX4Bz5FcQb3Pi5_36fHujuAAIdAwACzcclBYKV6alVSIzxGAQ",
                "CAACAgEAAxkBAAI5Nl59w-l5BqcaFXIKYQey7yMi251hAAIIAwACzcclBeLpDMOvSOemGAQ",
                "CAACAgEAAxkBAAI5OF59w-mVj6MnlNBimmdkZKUCnqu2AAIYAwACzcclBSODF1DAPF8FGAQ",
                "CAACAgEAAxkBAAI5N159w-m2sm9Dlu8BAjnwbsnypecpAAIXAwACzcclBfEy6NcNXNUHGAQ",
                "CAACAgEAAxkBAAI5Ol59w-rYoerxasA7GFMGzVifaimdAAIZAwACzcclBQZu-BbTBNFFGAQ",
                "CAACAgEAAxkBAAI5Ql59xCsg5KJQOORSoW_OVMi0j74eAAISAwACzcclBYatnDPp1g04GAQ",
                "CAACAgEAAxkBAAI5Q159xCzDgD8R4GZ58co_3EzRr3EKAAIUAwACzcclBRoFC-uQ5XxiGAQ",
                "CAACAgEAAxkBAAI5RF59xCxGX7uN2IsNwRdoAdxal3gpAAIKAwACzcclBTTqcqkPmpogGAQ",
                "CAACAgEAAxkBAAI5RV59xCzWonWaxzsEYB9FBugO54bjAAIJAwACzcclBSp-FC9DO8KxGAQ",
                "CAACAgEAAxkBAAI5R159xC15eomXH1giKyrFuFPPXXxSAAIVAwACzcclBXdzZl0sao99GAQ",
                "CAACAgEAAxkBAAI5Rl59xCxHiszC4D6vTFi6ql5BbsylAAITAwACzcclBa0f37s5EyJAGAQ",
                "CAACAgEAAxkBAAI5SF59xC3LpyRgPc-6DPMrOOCpILkMAAILAwACzcclBddMPGz3ku2WGAQ",
                "CAACAgEAAxkBAAI5UF59xHUkAvKd_W5v-Y3BVo8JYkyGAAIRAwACzcclBcoUY-7wmpB6GAQ",
                "CAACAgEAAxkBAAI5UV59xHXNu2SU-BKwM6ukuDeEvRFmAAINAwACzcclBRFymXUiMebZGAQ",
                "CAACAgEAAxkBAAI5U159xHZVCQABiodwNRMAAUZunkMhYEIAAhYDAALNxyUFzq8a3qfU03gYBA",
                "CAACAgEAAxkBAAI5Ul59xHVs2hkhetZDJZAOe8xDOg7oAAIPAwACzcclBc0c8lX3rIxrGAQ",
                "CAACAgEAAxkBAAI5WF59xHfCaBzYiKFcdNUQ1YIj6ObGAAIhAwACzcclBXW527t6r8htGAQ",
                "CAACAgEAAxkBAAI5VV59xHb7Tu2UVCo0Eql-CuuKPDWBAAIsAwACzcclBecYYzapF-RbGAQ",
                "CAACAgEAAxkBAAI5Vl59xHeij5VHpeprcl2840Y-2sQBAAIfAwACzcclBTi4rSKMevsgGAQ",
                "CAACAgEAAxkBAAI5VF59xHaB-gErBEkh9DXZaHDe9eLaAAIOAwACzcclBfGcKSkKZF2bGAQ",
                "CAACAgEAAxkBAAI5V159xHeYdoptiC57-xPpRa394tDgAAIgAwACzcclBdluT-36S2ydGAQ"
            ])
            .WithChances(0.3)
            .WithName("UwU");
    }
}