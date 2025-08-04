using System.Diagnostics.CodeAnalysis;
using Bottom;
using FOSCBot.Core.Helpers;
using FOSCBot.Core.Services;
using FOSCBot.Infrastructure.Contract.Service;
using Incremental.Common.Random;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Client;
using Navigator.Abstractions.Priorities;
using Navigator.Actions;
using Navigator.Actions.Builder.Extensions;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Navigator.Client;
using Navigator.Extensions.Cooldown.Extensions;
using Navigator.Extensions.Probabilities.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Actions;

public static partial class Fallbacks
{
    [Experimental("SKEXP0001")]
    public static void RegisterFallbacks(this BotActionCatalogFactory catalog)
    {
        catalog.OnText(Bottomify.IsEncoded, async (INavigatorClient client, Chat chat, string text) =>
            {
                await client.SendMessage(chat,
                    $"`Fellow humans I have decoded these words of wisdom:` \n_{Bottomify.DecodeString(text)}_",
                    parseMode: ParseMode.Markdown);
            })
            .WithChatAction(ChatAction.Typing)
            .WithName("Fallback.DeBottomify");

        catalog.OnText((string text) =>
            {
                var word = text.Trim().Split(" ").FirstOrDefault() ?? string.Empty;
                return !string.IsNullOrWhiteSpace(word) && word.IsAllUpper() && word.Length >= 4 && !word.Contains("XDDD");
            }, async (INavigatorClient client, Chat chat, string text, IGiphyService giphy) =>
            {
                var gifUrl = await giphy.Get(text.Trim().Split(" ").FirstOrDefault() ?? string.Empty);

                if (gifUrl is not null) await client.SendAnimation(chat, new InputFileUrl(gifUrl));
            })
            .WithProbabilities(0.4)
            .WithCooldown(TimeSpan.FromMinutes(15))
            .WithPriority(EPriority.Low)
            .WithChatAction(ChatAction.UploadVideo)
            .WithName("Fallback.SomewhatRandomGIF");

        // catalog.OnMessage(() => true, async (INavigatorClient client, Chat chat) =>
        //     {
        //         const string text =
        //             "`点击打开LINUXCT查看对应内容击打开HONG SPOTIFY应内容应内容TIANANMEN击打开应内容FENSHAO RISG*RE内容应内SIWANG死亡DESCENDANT查看对VUELING" +
        //             "内容查看WALLPAPER PORT查看对REQIN FUWU热情服务KITERIS内容查FOSC对应内TWITTER DA YONGHU大用户SAFETYNET没有电话MEIYOU DIANHUA卡尔佩SONY XPERIA ZAO TOULE" +
        //             "糟透HONG NETFLIX阿姆斯特丹AMSTERDAM火鸡TURKEY非常便宜FEICHANG PIANYI很暗HEN AN阴暗的空MARK ASS BROWNIE GALAXY Z FOLD 2 REVIEW里诺克斯蒂VERY DARK美国WANTED" +
        //             "通缉5星★★★★★`";
        //
        //         await client.SendMessage(chat, text, parseMode: ParseMode.Markdown);
        //     })
        //     .WithProbabilities(0.01)
        //     .WithCooldown(TimeSpan.FromDays(15))
        //     .WithPriority(Priority.Low)
        //     .WithChatAction(ChatAction.Typing)
        //     .WithName("Fallback.ChinesePolice");

        // Catch All Fallback
        catalog
            .OnMessage(() => true, async (INavigatorClient client, Chat chat, Message message, ILipsumService lipsum, AgentService agentService) =>
            {
                var sentence = string.Empty;

                if (message.Text?.Length > 200)
                {
                    var response = await agentService.ProcessMessage(chat, message);

                    sentence = response;
                }

                var odds = RandomProvider.GetThreadRandom()!.Next(0, 20);

                switch (odds)
                {
                    case >= 0 and < 3:
                        sentence = await lipsum.GetBacon();
                        break;
                    case >= 3 and < 10:
                        sentence = await lipsum.GetMetaphorSentence();
                        break;
                    default:
                        {
                            if (message.Text?.Split(' ').Length > 3) sentence = MockFilter.Apply(message.Text);
                            break;
                        }
                }

                if (!string.IsNullOrWhiteSpace(sentence))
                    await client.SendMessage(chat, sentence, parseMode: ParseMode.Markdown, replyParameters: message);
            })
            .WithProbabilities(0.002)
            .WithPriority(EPriority.VeryLow)
            .WithName("Fallback.CatchAllOLD");

        catalog
            .OnMessage(() => true)
            .SetHandler(CatchAllHandler)
            .WithPriority(EPriority.Lowest)
            .WithName("Fallback.CatchAll");
    }
}