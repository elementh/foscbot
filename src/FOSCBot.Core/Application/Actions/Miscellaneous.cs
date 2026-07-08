using System.Text.RegularExpressions;
using FOSCBot.Core.Application.Abstractions;
using FOSCBot.Core.Common;
using FOSCBot.Core.Resources;
using Incremental.Common.Random;
using Navigator.Abstractions.Actions.Builder.Extensions;
using Navigator.Abstractions.Catalog.Extensions;
using Navigator.Abstractions.Client;
using Navigator.Actions.Builder.Extensions;
using Navigator.Catalog.Factory;
using Navigator.Extensions.Cooldown.Extensions;
using Navigator.Extensions.Probabilities.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Application.Actions;

public static partial class Miscellaneous
{
    public static void RegisterMiscellaneous(this BotActionCatalogFactory catalog)
    {
        catalog
            .OnText((string text) => text.ToLower().Equals("based") || text.Equals("BASED"))
            .SendVideo("https://raw.githubusercontent.com/elementh/foscbot/master/assets/based_department.mp4")
            .WithProbabilities(0.2)
            .WithName("Miscellaneous.Based");

        catalog
            .OnText((string text) => text.Contains("bill gates", StringComparison.CurrentCultureIgnoreCase) ||
                                     text.Contains("microsoft", StringComparison.CurrentCultureIgnoreCase))
            .SendRandomSticker([
                "CAACAgQAAxkBAAI7GV6AjQuBNOLeYNKM1SBiRWB7tnuiAAIVDgAC_wRTAAGOgxwVhlJSlBgE",
                "CAACAgIAAxkBAAI7G16AkmXDeUy_ub_bezQ1rCEoQJhaAAKEBwACYyviCdnd2EStFD0DGAQ",
                "CAACAgIAAxkBAAI7HV6AkohZo2o9brFBO2wXVaZw7WusAAKRBwACYyviCcQ168JPLJQxGAQ",
                "CAACAgIAAxkBAAI7H16AkqTrAQ7RuRodfkDnR22FgHi5AAKXBwACYyviCdpCtHXzTycRGAQ",
                "CAACAgIAAxkBAAI7IV6AkqefjL2tvmJFtmVg04eF3YLsAAKPBwACYyviCR9FAgcEOrD5GAQ",
                "CAACAgIAAxkBAAI7I16Akqs0MGoJKRvozewtn8rD-s-BAAKYBwACYyviCTG5W40KakR2GAQ",
                "CAACAgIAAxkBAAI7Jl6Akxqjt-lLYrVUo5I4NjgNaNigAAKDBwACYyviCX-4JBrvWaA3GAQ"
            ])
            .WithName("Miscellaneous.B$llGates");

        catalog
            .OnText((string text) => text.Contains("blahaj", StringComparison.InvariantCultureIgnoreCase))
            .SendRandomSticker([
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
            .WithName("Miscellaneous.Blahaj");

        catalog
            .OnText((string text) => ArchRegex().IsMatch(text))
            .SendText("`Btw I run on Arch Linux.`", asReply: true, parseMode: ParseMode.Markdown)
            .WithProbabilities(0.4)
            .WithName("Miscellaneous.BtwArch");

        catalog
            .OnText((string text) =>
                text.Contains("skill issue", StringComparison.CurrentCultureIgnoreCase) ||
                text.Contains("git gud", StringComparison.CurrentCultureIgnoreCase) ||
                text.Contains("get good", StringComparison.CurrentCultureIgnoreCase) ||
                text.Contains("issue de skill", StringComparison.CurrentCultureIgnoreCase) ||
                text.Contains("works as intended", StringComparison.CurrentCultureIgnoreCase) ||
                NoPuedoMasRegex().IsMatch(text))
            .SendRandomText([
                "`Skill issue.`",
                "`Massive user diff.`",
                "`You lost to basic reality again.`",
                "`That sounds like a you problem with extra ceremony.`"
            ], ParseMode.Markdown, true)
            .WithProbabilities(0.6)
            .WithName("Miscellaneous.SkillIssue");

        catalog.OnText((string text) => text.Contains("cagaste", StringComparison.CurrentCultureIgnoreCase))
            .SendRandomPhoto([
                "https://raw.githubusercontent.com/elementh/foscbot/master/assets/cagastegoku.jpg",
                "https://raw.githubusercontent.com/elementh/foscbot/master/assets/cagasteshark.jpg"
            ])
            .WithName("Miscellaneous.Cagaste");

        catalog
            .OnText((string text) => text.Contains("congrats", StringComparison.CurrentCultureIgnoreCase) ||
                                     text.Contains("congratulations", StringComparison.CurrentCultureIgnoreCase))
            .SendSticker("CAACAgIAAxkBAAEEZYRiTrRMXTzG543N43iKgLsPlyK_1wACwwcAAhhC7gjyFcEHatcw1CME", toReply: true)
            .WithName("Miscellaneous.Congrats");

        catalog
            .OnText((string text) => text.Contains("pobres", StringComparison.CurrentCultureIgnoreCase) ||
                                     text.Contains("dineros", StringComparison.CurrentCultureIgnoreCase))
            .SendVideo("https://raw.githubusercontent.com/elementh/foscbot/master/assets/dineros.mp4")
            .WithName("Miscellaneous.Dineros");

        catalog
            .OnText((string text) => text.Contains("españa", StringComparison.CurrentCultureIgnoreCase))
            .SendSticker("CAACAgQAAxkBAAJWPF6i8ixK0-SqAayKyCdmHYcYFix3AAIhAAN87RspJn8XTAs-3tUZBA")
            .WithProbabilities(0.4)
            .WithName("Miscellaneous.DjEspanita");

        catalog.OnText(
                (string text) => text.Contains("elegant", StringComparison.CurrentCultureIgnoreCase) &&
                                 !text.ContainsUrl())
            .SendVideo("https://raw.githubusercontent.com/elementh/foscbot/master/assets/elegant.mp4",
                "Elegance is achieved when all that is superfluous has been discarded and the human being discovers simplicity and concentration: the simpler and more sober the posture, the more beautiful it will be.")
            .WithName("Miscellaneous.Elegant");

        catalog
            .OnText((string text) => ElonMuskRegex().IsMatch(text))
            .SendRandomSticker([
                "CAACAgIAAxkBAAECGX9gWyite1lkgqD944zLdk31Cn_MbQACmggAAnlc4gmF6N5K1J_nix4E",
                "CAACAgIAAxkBAAECGYFgWyjJmDQFzVqaqbVq51qNgq-iYAACgggAAnlc4gndXhh_OFD8Rx4E",
                "CAACAgIAAxkBAAECGYFgWyjJmDQFzVqaqbVq51qNgq-iYAACgggAAnlc4gndXhh_OFD8Rx4E",
                "CAACAgIAAxkBAAECGYFgWyjJmDQFzVqaqbVq51qNgq-iYAACgggAAnlc4gndXhh_OFD8Rx4E",
                "CAACAgIAAxkBAAECGYtgWyj4FPWvacn18y11asl4Qq8rZgAC7wgAAnlc4gnztBdi0FWsRh4E",
                "CAACAgIAAxkBAAECGY1gWyj6c-QMcGzNPWtfiGaPZE0WcwACkggAAnlc4glg2uUwtwJdvR4E"
            ])
            .WithName("Miscellaneous.FelonMask");

        catalog
            .OnText((string text) => text.Contains("for our stolen code", StringComparison.InvariantCultureIgnoreCase)
                                     || OrksRegex().IsMatch(text))
            .SendVideo("https://raw.githubusercontent.com/elementh/foscbot/master/assets/orks.mp4")
            .WithProbabilities(0.8)
            .WithName("Miscellaneous.Forks");

        catalog
            .OnText((string text) => GoAheadRegex().IsMatch(text))
            .SendVideo("https://raw.githubusercontent.com/elementh/foscbot/master/assets/goahead.mp4",
                "SSSSSSSSSSSUCK YOUR OWN COCKKKKK")
            .WithProbabilities(0.8)
            .WithName("Miscellaneous.GoAhead");

        catalog
            .OnSticker(
                (Sticker sticker) =>
                    sticker.SetName?.Equals("foscupct") is true && sticker.Emoji?.Equals("😚") is true,
                async (INavigatorClient client, Chat chat) =>
                {
                    var bytes = Convert.FromBase64String(CoreResources.HeyBroImage);

                    await using var stream = await new StreamContent(new MemoryStream(bytes)).ReadAsStreamAsync();

                    await client.SendPhoto(chat, new InputFileStream(stream, "heybroniced.jpg"));
                })
            .WithProbabilities(0.5)
            .WithChatAction(ChatAction.UploadPhoto)
            .WithName("Miscellaneous.HeyBro");

        catalog
            .OnMessage((Message message) =>
            {
                if (message.Text?.Contains("fucking ice", StringComparison.CurrentCultureIgnoreCase) is true)
                    return true;

                if (RandomProvider.GetThreadRandom()!.NextDouble() < 0.2d) return false;

                return message.Text?.StartsWith("ice", StringComparison.CurrentCultureIgnoreCase) is true ||
                       message.Text?.Contains(" ice ", StringComparison.CurrentCultureIgnoreCase) is true ||
                       message.Text?.Contains(" ice?", StringComparison.CurrentCultureIgnoreCase) is true ||
                       message.Text?.Contains(" hielo ", StringComparison.CurrentCultureIgnoreCase) is true ||
                       message.Text?.Contains(" cold ", StringComparison.CurrentCultureIgnoreCase) is true ||
                       message.Sticker?.Emoji?.Equals("🥶") is true ||
                       message.Sticker?.Emoji?.Equals("🧊") is true ||
                       message.Sticker?.Emoji?.Equals("❄️") is true;
            })
            .SendVideo("https://raw.githubusercontent.com/elementh/foscbot/master/assets/ice.mp4")
            .WithName("Miscellaneous.Ice");

        catalog
            .OnText((string text) => IpadRegex().IsMatch(text))
            .SetHandler(async (INavigatorClient client, Chat chat) =>
            {
                if (RandomProvider.GetThreadRandom()!.NextDouble() >= 0.5)
                {
                    await client.SendChatAction(chat, ChatAction.UploadPhoto);
                    await client.SendPhoto(chat, CoreLinks.Ipad, caption: "tEnGo Un IpAd");
                }
                else
                {
                    await client.SendChatAction(chat, ChatAction.UploadVoice);

                    var bytes = Convert.FromBase64String(CoreResources.IpadAudio);
                    await using var stream = await new StreamContent(new MemoryStream(bytes)).ReadAsStreamAsync();

                    await client.SendVoice(chat, new InputFileStream(stream, "ipad"), duration: 5);
                }
            })
            .WithCooldown(TimeSpan.FromMinutes(15))
            .WithName("Miscellaneous.Ipad");

        catalog
            .OnText((string text) => JejeRegex().IsMatch(text))
            .SendRandomSticker([
                "CAACAgIAAxkBAAI5Dl59vPOH6MA6Uzua49AHRz-q5mMUAAIKAQACMNSdEVZUV2nGbrlvGAQ",
                "CAACAgQAAxkBAAI5EF59vUKLQ46GEgbuzhY0O5r3HyauAAJKAQACqCEhBntEKK5RNh4XGAQ",
                "CAACAgIAAxkBAAI5El59vYqk6ywiJOKdXzXNe2gsPL2gAAL2AwACierlB263K9ogJ_bwGAQ",
                "CAACAgIAAxkBAAI5FF59vZFmQWYDsTaj4X9GJl9bPAbEAAJBAQAC-YQfHIsWbGjJcnqnGAQ",
                "CAACAgQAAxkBAAI5Fl59vb9993hlyxnbQ_VEZlEMqzymAAI7AgACMo1bAAEaE0PNwutkzBgE",
                "CAACAgIAAxkBAAI5GF59vfIG0bug-aIj8txxEBNNiNUXAAIQCAACGELuCOAfnJHe30ZuGAQ",
                "CAACAgIAAxkBAAI5Gl59vfg4AefyKXIXUAMOdoCs6gNAAALNBwACGELuCPlfWYiQZaQiGAQ"
            ])
            .WithName("Miscellaneous.Jeje");

        catalog.OnText((string text) => text.Contains("jonardo", StringComparison.CurrentCultureIgnoreCase))
            .SendRandomSticker([
                "CAACAgQAAxkBAAEMtvNmzFNi3fECy0IdKww6_QLSq2iblAACRAMAAkstbAABM86dNtNv9hU1BA",
                "CAACAgIAAxkBAAEMtuVmzFLlOxQ5m2JJdXsbe8D0dj-1ogAC9jIAAlupkUqZKiEYNaUYrzUE",
                "CAACAgIAAxkBAAEMtudmzFL39OSSQl_cr1vtAurW7NW4OAACliYAAtSFmErSRQFOnOH5MDUE",
                "CAACAgIAAxkBAAEMtulmzFL9WoG8ysqxPuu4Rmrxw8WT9QACvC4AAuPx8EtXGBlONqz1jTUE",
                "CAACAgIAAxkBAAEMtutmzFMHVI-loOnZFAibOLy8_ybdfwAC_i4AAlaX6EscsCyDfdZ4dDUE",
                "CAACAgIAAxkBAAEMtu1mzFMLLmYLShp0EGIzG2XoCXXFSgACIEIAAhnkgEho_8zIBxjxDDUE",
                "CAACAgIAAxkBAAEMtu9mzFMn4qGJoZ0yqvlrbFGe1ZoDVAACWDkAAvYq-UmqEr0HAW9EjzUE",
                "CAACAgQAAxkBAAEMtvVmzFQ53ImvtIlN1UAJlQ449scjnAACoQwAAssCCVDV12Iit4UzIjUE",
                "CAACAgQAAxkBAAEMtvFmzFM-8WJEs9UXott_8ZmXPxmciQACaQAD12KbDpEH23X1iY00NQQ"
            ])
            .WithName("Miscellaneous.Jonardo");

        catalog
            .OnText((string text) => KissRegex().IsMatch(text))
            .SendText("Keep it simple, and don't be a dick, bro. 🤗")
            .WithProbabilities(0.8)
            .WithName("Miscellaneous.KeepItSimple");

        catalog
            .OnText((string text) =>
                text.Contains("lgtm", StringComparison.CurrentCultureIgnoreCase) ||
                text.Contains("ship it", StringComparison.CurrentCultureIgnoreCase) ||
                ApprovedRegex().IsMatch(text) ||
                text.Contains("mergealo", StringComparison.CurrentCultureIgnoreCase) ||
                text.Contains("merge it", StringComparison.CurrentCultureIgnoreCase))
            .SendRandomText([
                "`LGTM: Looks Good To Morons.`",
                "`Approved by vibes. What could possibly go wrong.`",
                "`Ship it. I'm sure prod enjoys surprises.`",
                "`Another merge powered by confidence instead of evidence.`"
            ], ParseMode.Markdown, true)
            .WithProbabilities(0.4)
            .WithName("Miscellaneous.LGTM");

        catalog
            .OnText((string text) => text.Contains("elantro", StringComparison.CurrentCultureIgnoreCase)
                                     || text.Contains("lantra", StringComparison.CurrentCultureIgnoreCase))
            .SendPhoto("https://raw.githubusercontent.com/elementh/foscbot/main/assets/lantra.jpg", "Elantro")
            .WithName("Miscellaneous.Lantra");

        catalog
            .OnText((string text) => text is "LETS" or "LET'S" or "LET’S")
            .SetHandler(async (INavigatorClient client, Chat chat) =>
            {
                string[] stickerList =
                [
                    "CAACAgQAAxkBAAECojpg_bnnG-u3pLeJOG5IPolOtN00RAACBQwAApUS8FOOn4aL83n8bSAE",
                    "CAACAgQAAxkBAAECojxg_bnqH-1SeiLQLt_KypOZBI5uowACmQ8AAmFh8FO6uZ5fZWppOiAE",
                    "CAACAgQAAxkBAAECoj5g_bp_i3lF_F8dZQK8ZLEvKK3eJgAC3gkAAvPw8VPNHKHR3O21xiAE",
                    "CAACAgQAAxkBAAECokBg_bqCvo4wxPdsQfKZK4WqdCky0wACvQgAAs326VOcqEIdovpiBCAE",
                    "CAACAgQAAxkBAAECokJg_bqH1nmA0VD9OwRaaJ7XzcbRngAC6AgAAjFY8FOa7ph-XJo5GSAE",
                    "CAACAgQAAxkBAAECokRg_bqfolorzkJYdp9yC4DKHmiO9gACEgsAAum88VObzYkkVjOSUyAE",
                    "CAACAgQAAxkBAAECokZg_bqj-FylWbafjgm9f_h1oXrahQAC7QoAAtmJ8FNTWKR0OIY5USAE",
                    "CAACAgQAAxkBAAECokhg_brMFoDgdE2fx2KYV2YYVR6s9wACggsAAm5s8VOsGad1nfXzfCAE",
                    "CAACAgQAAxkBAAECokpg_brRLEoWV1mx8Sxlgvwg7XU-WAACLQwAAlWt8VN-tLCZkqsk_yAE",
                    "CAACAgQAAxkBAAECokxg_brVzW5P93IB4ObTMKKxTzSKYAAC6QkAAlWw8FN6EYT5JghtsyAE"
                ];

                var randomSticker = stickerList[RandomProvider.GetThreadRandom()!.Next(0, stickerList.Length)];

                await client.SendMessage(chat, "FUCKING");

                await client.SendChatAction(chat, ChatAction.Typing);
                await Task.Delay(200);
                await client.SendMessage(chat, "GO");

                await client.SendChatAction(chat, ChatAction.ChooseSticker);
                await Task.Delay(200);
                await client.SendSticker(chat, randomSticker);
            })
            .WithChatAction(ChatAction.Typing)
            .WithName("Miscellaneous.Lets");

        catalog
            .OnText((string text) => LetsFuckingGoRegex().IsMatch(text))
            .SendRandomSticker([
                "CAACAgQAAxkBAAECojpg_bnnG-u3pLeJOG5IPolOtN00RAACBQwAApUS8FOOn4aL83n8bSAE",
                "CAACAgQAAxkBAAECojxg_bnqH-1SeiLQLt_KypOZBI5uowACmQ8AAmFh8FO6uZ5fZWppOiAE",
                "CAACAgQAAxkBAAECoj5g_bp_i3lF_F8dZQK8ZLEvKK3eJgAC3gkAAvPw8VPNHKHR3O21xiAE",
                "CAACAgQAAxkBAAECokBg_bqCvo4wxPdsQfKZK4WqdCky0wACvQgAAs326VOcqEIdovpiBCAE",
                "CAACAgQAAxkBAAECokJg_bqH1nmA0VD9OwRaaJ7XzcbRngAC6AgAAjFY8FOa7ph-XJo5GSAE",
                "CAACAgQAAxkBAAECokRg_bqfolorzkJYdp9yC4DKHmiO9gACEgsAAum88VObzYkkVjOSUyAE",
                "CAACAgQAAxkBAAECokZg_bqj-FylWbafjgm9f_h1oXrahQAC7QoAAtmJ8FNTWKR0OIY5USAE",
                "CAACAgQAAxkBAAECokhg_brMFoDgdE2fx2KYV2YYVR6s9wACggsAAm5s8VOsGad1nfXzfCAE",
                "CAACAgQAAxkBAAECokpg_brRLEoWV1mx8Sxlgvwg7XU-WAACLQwAAlWt8VN-tLCZkqsk_yAE",
                "CAACAgQAAxkBAAECokxg_brVzW5P93IB4ObTMKKxTzSKYAAC6QkAAlWw8FN6EYT5JghtsyAE"
            ])
            .WithName("Miscellaneous.LetsGo");

        catalog
            .OnText((string text) =>
                text.ToLower().Contains("so sad") || text.ToLower().Contains("ligma") ||
                text.ToLower().Contains("p4cock"))
            .SetHandler(async (INavigatorClient client, Chat chat) =>
            {
                await client.SendChatAction(chat, ChatAction.UploadVoice);

                if (RandomProvider.GetThreadRandom()!.NextDouble() >= 0.75)
                {
                    var bytes = Convert.FromBase64String(CoreResources.LigmaHardAudio);
                    await using var stream = await new StreamContent(new MemoryStream(bytes)).ReadAsStreamAsync();

                    await client.SendVoice(chat, new InputFileStream(stream, "LIGMA BALLS"), duration: 4);
                }
                else
                {
                    var bytes = Convert.FromBase64String(CoreResources.LigmaSoftAudio);
                    await using var stream = await new StreamContent(new MemoryStream(bytes)).ReadAsStreamAsync();

                    await client.SendVoice(chat, new InputFileStream(stream, "ligma balls"), duration: 3);
                }
            })
            .WithName("Miscellaneous.Ligma");

        catalog
            .OnText((string text) => MegatronRegex().IsMatch(text)
                                     || text.Contains("tronco", StringComparison.CurrentCultureIgnoreCase))
            .SendRandomVideo([
                "https://raw.githubusercontent.com/elementh/foscbot/master/assets/megatron_cbt_experience.mp4",
                "https://raw.githubusercontent.com/elementh/foscbot/master/assets/megatron_cbt_immediate.mp4"
            ])
            .WithName("Miscellaneous.Megratron");

        catalog
            .OnText((string text) => text.Contains("monke", StringComparison.CurrentCultureIgnoreCase)
                                     || MonosRegex().IsMatch(text)
                                     || text.Contains("puto mono", StringComparison.CurrentCultureIgnoreCase))
            .SendVideo("https://raw.githubusercontent.com/elementh/foscbot/main/assets/monke.mp4", "RETURN. TO. MONKE.")
            .WithName("Miscellaneous.Monke");

        catalog
            .OnText((string text) => text.Contains("nft", StringComparison.CurrentCultureIgnoreCase))
            .SendRandomPhoto([
                "https://raw.githubusercontent.com/elementh/foscbot/master/assets/nft.jpg",
                "https://raw.githubusercontent.com/elementh/foscbot/master/assets/nftoad.jpg",
                "https://raw.githubusercontent.com/elementh/foscbot/master/assets/nftractor.jpg",
                "https://raw.githubusercontent.com/elementh/foscbot/master/assets/nftattoo.jpg",
                "https://raw.githubusercontent.com/elementh/foscbot/master/assets/nftu.png"
            ])
            .WithProbabilities(0.6)
            .WithName("Miscellaneous.NFT");

        catalog
            .OnText((string text) => text.Contains("NICE"))
            .SendVideo("https://raw.githubusercontent.com/elementh/foscbot/master/assets/nice.mp4")
            .WithProbabilities(0.7)
            .WithName("Miscellaneous.Nice");

        catalog
            .OnText((string text) => text.Contains("nginx", StringComparison.CurrentCultureIgnoreCase))
            .SetHandler(async (INavigatorClient client, Chat chat) =>
            {
                var bytes = Convert.FromBase64String(CoreResources.NginxImage);

                await using var stream = await new StreamContent(new MemoryStream(bytes)).ReadAsStreamAsync();

                await client.SendPhoto(chat, new InputFileStream(stream, "nginx.jpg"));
            })
            .WithProbabilities(0.4)
            .WithChatAction(ChatAction.UploadPhoto)
            .WithName("Miscellaneous.Nginx");

        catalog
            .OnText((string text) => text.Equals("NO") || text.Equals("NOPE"))
            .SendVideo("https://raw.githubusercontent.com/elementh/foscbot/master/assets/nope.mp4")
            .WithProbabilities(0.5)
            .WithName("Miscellaneous.Nope");

        catalog
            .OnText((string text) => text.Contains("nvidia", StringComparison.CurrentCultureIgnoreCase))
            .SendVideo("https://raw.githubusercontent.com/elementh/foscbot/master/assets/nvidia.mp4")
            .WithProbabilities(0.4)
            .WithName("Miscellaneous.Nvidia");

        catalog
            .OnText((Update update, string text) =>
                update.IsBotMentioned() && text.Contains("python", StringComparison.CurrentCultureIgnoreCase))
            .SendRandomText([
                "Aprendí python para que los plebs puedan entenderme 🥱",
                "I'm `c̵̰̉ṳ̸̪̑̿̚̚r̴̟͕̗̄̓̀̉̄s̵̰̩̎̈e̷͙̯͍̲̋̊͘ḍ̴͂̊ ̶̘̈́a̷̹̲͗͗͊͋̾ṡ̸̨̢̰͂̀͗̕ ̵̧͓͑̈́̍͝f̷̦̠̹̤͊͛̈́u̸͇͔̓c̶̼͓͂̇k̴͍͐̽̏̈͗`, que alguien me desconecte joder 🤡🤡"
            ], ParseMode.MarkdownV2, true)
            .WithName("Miscellaneous.SpeakPython");

        catalog
            .OnText((string text) => ReeRegex().IsMatch(text))
            .SendVideo("https://raw.githubusercontent.com/elementh/foscbot/master/assets/ree.mp4")
            .WithName("Miscellaneous.Ree");

        catalog
            .OnText((string text) => text.Contains("rgb", StringComparison.CurrentCultureIgnoreCase))
            .SendText("eL rGb n0 Da FpS")
            .WithName("Miscellaneous.RGB");

        catalog
            .OnMessage((Message message) =>
            {
                return message.Text?.ToLower().Equals("sad") is true
                       || message.Text?.ToLower().Contains(" sad ") is true
                       || message.Sticker?.Emoji is "😔" or "😢" or "😞" or "😭";
            })
            .SendSticker("CAACAgQAAxkBAAI5DF59uqkJYnqzc5LcnEC_bdp0rerIAAJsAwACmOejAAG_qYNUT_L_exgE")
            .WithProbabilities(0.6)
            .WithName("Miscellaneous.Sad");

        catalog
            .OnText((string text) =>
            {
                return text.ToLower().Equals("source?")
                       || text.ToLower().Equals("source")
                       || text.ToLower().Equals("sauce?")
                       || text.ToLower().Equals("sauce")
                       || text.ToLower().Equals("saus?")
                       || text.ToLower().Equals("saus");
            })
            .SendRandomVideo([
                "https://raw.githubusercontent.com/elementh/foscbot/master/assets/source.jpg",
                "https://raw.githubusercontent.com/elementh/foscbot/master/assets/source_chad.jpg"
            ])
            .WithName("Miscellaneous.Sauce");

        catalog
            .OnText((string text) => SteMenRegex().IsMatch(text))
            .SendPhoto("https://raw.githubusercontent.com/elementh/foscbot/master/assets/stemen.jpg")
            .WithName("Miscellaneous.Stemen");

        catalog
            .OnText((string text) => text.Contains("stonks", StringComparison.CurrentCultureIgnoreCase))
            .SendVideo("https://raw.githubusercontent.com/elementh/foscbot/master/assets/stonks.mp4")
            .WithProbabilities(0.4)
            .WithName("Miscellaneous.Stonks");

        catalog
            .OnText((string text) =>
            {
                return text.Equals("SUCC")
                       || text.Equals("SAC")
                       || text.Contains("a chuparla", StringComparison.CurrentCultureIgnoreCase)
                       || text.Contains("a mamarla", StringComparison.CurrentCultureIgnoreCase);
            })
            .SendRandomVideo([
                "https://raw.githubusercontent.com/elementh/foscbot/master/assets/succ.mp4",
                "https://raw.githubusercontent.com/elementh/foscbot/master/assets/succ_with_teeth.mp4"
            ])
            .WithName("Miscellaneous.Succ");

        catalog
            .OnText((string text) => text.Contains("synology", StringComparison.CurrentCultureIgnoreCase))
            .SendText("Shitn0plogy", asReply: true)
            .WithName("Miscellaneous.Synology");

        catalog
            .OnText((string text) =>
                text.Contains("works on my machine", StringComparison.CurrentCultureIgnoreCase) ||
                text.Contains("cannot reproduce", StringComparison.CurrentCultureIgnoreCase) ||
                text.Contains("can't reproduce", StringComparison.CurrentCultureIgnoreCase) ||
                text.Contains("can’t reproduce", StringComparison.CurrentCultureIgnoreCase) ||
                NoMePasaRegex().IsMatch(text) ||
                text.Contains("en mi máquina va", StringComparison.CurrentCultureIgnoreCase) ||
                text.Contains("en mi maquina va", StringComparison.CurrentCultureIgnoreCase))
            .SetHandler(async (INavigatorClient client, Chat chat, Message message) =>
            {
                string[] lines =
                [
                    "`Ah yes, the internationally recognized standard: your laptop.`",
                    "`Works on your machine is not a deployment strategy, genius.`",
                    "`Congrats on shipping to localhost.`",
                    "`Incredible. The bug respects your hardware personally.`"
                ];

                await client.SendMessage(
                    chat,
                    lines[RandomProvider.GetThreadRandom()!.Next(0, lines.Length)],
                    parseMode: ParseMode.Markdown,
                    replyParameters: message);
            })
            .WithCooldown(TimeSpan.FromMinutes(10))
            .WithName("Miscellaneous.WorksOnMyMachine");

        catalog
            .OnText((string text) => text.Contains("tesla", StringComparison.CurrentCultureIgnoreCase))
            .SendVideo("https://raw.githubusercontent.com/elementh/foscbot/master/assets/sergio_model3.mp4")
            .WithName("Miscellaneous.Tesla");

        catalog
            .OnText((string text) =>
            {
                return text.ToLower().Contains("cock and balls torture")
                       || text.ToLower().Contains("cock and ball torture")
                       || text.ToLower().Contains("cum blast me")
                       || text.ToLower().Contains("cbt");
            })
            .SetHandler(async (INavigatorClient client, Chat chat) =>
            {
                switch (RandomProvider.GetThreadRandom()!.Next(0, 4))
                {
                    case 0:
                        await client.SendChatAction(chat, ChatAction.UploadVideo);
                        await Task.Delay(200);
                        await client.SendVideo(chat,
                            "https://raw.githubusercontent.com/elementh/foscbot/master/assets/cbt_explanation.mp4");
                        break;
                    case 1:
                        await client.SendMessage(chat, "And make it snappy");
                        await client.SendChatAction(chat, ChatAction.UploadVideo);
                        await Task.Delay(200);
                        await client.SendVideo(chat, CoreLinks.Conke);
                        break;
                    case 2:
                        await client.SendChatAction(chat, ChatAction.UploadVideo);
                        await Task.Delay(200);
                        await client.SendVideo(chat,
                            "https://raw.githubusercontent.com/elementh/foscbot/master/assets/megatron_cbt_experience.mp4");
                        break;
                    case 3:
                        await client.SendChatAction(chat, ChatAction.UploadVideo);
                        await Task.Delay(200);
                        await client.SendVideo(chat,
                            "https://raw.githubusercontent.com/elementh/foscbot/master/assets/megatron_cbt_immediate.mp4");
                        break;
                }
            })
            .WithName("Miscellaneous.Torture");

        catalog
            .OnText((string text) =>
            {
                return BlyatRegex().IsMatch(text)
                       || TraktorRegex().IsMatch(text)
                       || text.Contains("блядь", StringComparison.CurrentCultureIgnoreCase)
                       || text.Contains("трактор", StringComparison.CurrentCultureIgnoreCase);
            })
            .SendRandomVideo([
                "https://raw.githubusercontent.com/elementh/foscbot/master/assets/bueno_flipao.mp4",
                "https://raw.githubusercontent.com/elementh/foscbot/master/assets/traktor.mp4"
            ])
            .WithName("Miscellaneous.Traktor");

        catalog
            .OnText((string text) => text.Contains("upct", StringComparison.OrdinalIgnoreCase) &&
                                     !text.Contains("/upct", StringComparison.OrdinalIgnoreCase))
            .SetHandler(async (INavigatorClient client, Chat chat) =>
            {
                var randomSticker = RandomProvider.GetThreadRandom()!.NextDouble() > 0.2d
                    ? "CAACAgQAAxkBAAJNW16eEHOauvBkLuaD-jL95s86vn2qAAJuAwACmOejAAEys6bCdTOD7RgE"
                    : "CAACAgQAAxkBAAJNXV6eEJLQHwl-8el7YOYYJUF9l8ymAAJZAgACkNStBjfoiv3ywvd8GAQ";

                await client.SendSticker(chat, randomSticker);

                if (RandomProvider.GetThreadRandom()!.NextDouble() > 0.8d)
                    await client.SendChatAction(chat, ChatAction.Typing);
                await Task.Delay(250);
                await client.SendMessage(chat, "cAmPuS dE eXcElEnCiA iNtErNaCiOnAl");
            })
            .WithChatAction(ChatAction.ChooseSticker)
            .WithName("Miscellaneous.UPCT");

        catalog
            .OnText((string text) => text.Contains("uwu", StringComparison.CurrentCultureIgnoreCase))
            .SendRandomSticker([
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
            .WithProbabilities(0.3)
            .WithName("Miscellaneous.UwU");

        catalog
            .OnText((string text) =>
            {
                if (text.ToLower().Contains("vueling") || text.ToLower().Contains("bueling")) return true;

                return RandomProvider.GetThreadRandom()!.NextDouble() > 0.6d
                       && (text.ToLower().Contains("volar") || text.ToLower().Contains("avion") ||
                           text.ToLower().Contains("avión"));
            })
            .SetHandler(async (INavigatorClient client, Chat chat) =>
            {
                await client.SendMessage(chat, "Did some carbon based life form just mention...");

                await client.SendChatAction(chat, ChatAction.ChooseSticker);

                await Task.Delay(400);

                await client.SendSticker(chat,
                    "CAACAgQAAxkBAAJJpl6bSONlqhE0C21-0T9V9YHxfqPKAAKZBgACL9trAAHwqRcYUmB_gRgE");
            })
            .WithName("Micellaneous.Vueling");

        catalog
            .OnText((string text) => WaghRegex().IsMatch(text))
            .SendVideo("https://raw.githubusercontent.com/elementh/foscbot/master/assets/orks.mp4")
            .WithName("Miscellaneous.Wagh");

        catalog
            .OnText((string text) => text is "YES" or "SI" or "SÍ")
            .SendSticker("CAACAgQAAxkBAAI5HF59wcwDyRdwkEU3m_4CMMoz06CwAAKvAwACSy1sAAHbWFZ7iah6TRgE")
            .WithProbabilities(0.5)
            .WithName("Miscellaneous.Yes");

        catalog
            .OnText((Update update) => update.IsBotQuotedOrMentioned() && update.IsBotFlattered())
            .SetHandler(async (INavigatorClient client, Chat chat, IGiphyService giphy) =>
            {
                var gifUrl = await giphy.Get("you are welcome idiot");

                if (gifUrl is not null) await client.SendAnimation(chat, new InputFileUrl(gifUrl));
            })
            .WithName("Miscellaneous.Flattered");
    }

    [GeneratedRegex(@"[Bb][Ll][Yy][Aa]+[Tt]+")]
    private static partial Regex BlyatRegex();

    [GeneratedRegex(@"\btra+[kc]to+r+", RegexOptions.IgnoreCase)]
    private static partial Regex TraktorRegex();

    [GeneratedRegex(@"^WA+G+H+!*$")]
    private static partial Regex WaghRegex();

    [GeneratedRegex(@"\bgo+\s+a+h+e+a+d+", RegexOptions.IgnoreCase)]
    private static partial Regex GoAheadRegex();

    [GeneratedRegex(@"\barch(?:linux)?\b", RegexOptions.IgnoreCase)]
    private static partial Regex ArchRegex();

    [GeneratedRegex(@"\bno puedo m[aá]s\b|\bno puc m[eé]s\b", RegexOptions.IgnoreCase)]
    private static partial Regex NoPuedoMasRegex();

    [GeneratedRegex(@"\b(?:elon|musk)\b", RegexOptions.IgnoreCase)]
    private static partial Regex ElonMuskRegex();

    [GeneratedRegex(@"\bf?orks\b", RegexOptions.IgnoreCase)]
    private static partial Regex OrksRegex();

    [GeneratedRegex(@"\bipads?\b", RegexOptions.IgnoreCase)]
    private static partial Regex IpadRegex();

    [GeneratedRegex(@"\b(?:je\s?)+\b", RegexOptions.IgnoreCase)]
    private static partial Regex JejeRegex();

    [GeneratedRegex(@"\bKISS\b")]
    private static partial Regex KissRegex();

    [GeneratedRegex(@"\bapproved\b", RegexOptions.IgnoreCase)]
    private static partial Regex ApprovedRegex();

    [GeneratedRegex(@"^let['’]?s fuckin[g']? go", RegexOptions.IgnoreCase)]
    private static partial Regex LetsFuckingGoRegex();

    [GeneratedRegex(@"meg(?:a|ra)tron", RegexOptions.IgnoreCase)]
    private static partial Regex MegatronRegex();

    [GeneratedRegex(@"\bmonos\b", RegexOptions.IgnoreCase)]
    private static partial Regex MonosRegex();

    [GeneratedRegex(@"\bREE+\b")]
    private static partial Regex ReeRegex();

    [GeneratedRegex(@"\be?ste men\b", RegexOptions.IgnoreCase)]
    private static partial Regex SteMenRegex();

    [GeneratedRegex(@"\bno me pasa\b(?!\s+(?:el|la|los|las|un|una|ni|de)\b)", RegexOptions.IgnoreCase)]
    private static partial Regex NoMePasaRegex();
}
