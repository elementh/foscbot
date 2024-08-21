using Bottom;
using FOSCBot.Core.Helpers;
using FOSCBot.Infrastructure.Contract.Service;
using Incremental.Common.Random;
using Navigator.Actions;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Navigator.Client;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Actions;

public static class Fallbacks
{
    public static void RegisterFallbacks(this BotActionCatalogFactory catalog)
    {
        // De-Bottomify Fallback
        catalog.OnText(Bottomify.IsEncoded, async (INavigatorClient client, Chat chat, string text) =>
        {
                await client.SendTextMessageAsync(chat, $"`Fellow humans I have decoded these words of wisdom:` \n_{Bottomify.DecodeString(text)}_",
                    parseMode: ParseMode.Markdown);
        });
        
        // Random Word Fallback
        catalog.OnText((string text) =>
        {
            if (RandomProvider.GetThreadRandom()!.NextDouble() > 0.6) return false;

            var word = text.Trim().Split(" ").FirstOrDefault() ?? string.Empty;
            return !string.IsNullOrWhiteSpace(word) && word.IsAllUpper() && word.Length >= 4 && !word.Contains("XDDD");
        }, async (INavigatorClient client, Chat chat, string text, IGiphyService giphy) =>
        {
            var gifUrl = await giphy.Get(text.Trim().Split(" ").FirstOrDefault() ?? string.Empty);

            if (gifUrl is not null)
            {
                await client.SendAnimationAsync(chat, new InputFileUrl(gifUrl));
            }
        }).WithCooldown(TimeSpan.FromMinutes(15)).WithPriority(Priority.Low - 100);

        // Chinese Police Fallback
        catalog.OnMessage(() => !(RandomProvider.GetThreadRandom()!.NextDouble() > 0.7), async (INavigatorClient client, Chat chat) =>
        {
            const string text = "`点击打开LINUXCT查看对应内容击打开HONG SPOTIFY应内容应内容TIANANMEN击打开应内容FENSHAO RISG*RE内容应内SIWANG死亡DESCENDANT查看对VUELING" +
                                "内容查看WALLPAPER PORT查看对REQIN FUWU热情服务KITERIS内容查FOSC对应内TWITTER DA YONGHU大用户SAFETYNET没有电话MEIYOU DIANHUA卡尔佩SONY XPERIA ZAO TOULE" +
                                "糟透HONG NETFLIX阿姆斯特丹AMSTERDAM火鸡TURKEY非常便宜FEICHANG PIANYI很暗HEN AN阴暗的空MARK ASS BROWNIE GALAXY Z FOLD 2 REVIEW里诺克斯蒂VERY DARK美国WANTED" +
                                "通缉5星★★★★★`";

            await client.SendTextMessageAsync(chat, text, parseMode: ParseMode.Markdown);
        }).WithCooldown(TimeSpan.FromDays(15)).WithPriority(Priority.Low);

        // Catch All Fallback
        catalog.OnMessage(() => true, async (INavigatorClient client, Chat chat, Message message, ILipsumService lipsum, ILlamaService llm) =>
        {
            if (RandomProvider.GetThreadRandom()!.Next(0, 600) < 598) return;

            var sentence = string.Empty;
        
            if (message.Text?.Length > 200)
            {
                var response = await llm.GetResponse(new[] { message.Text }, default);

                sentence = response;
            }
        
            var odds = RandomProvider.GetThreadRandom().Next(0, 20);

            if (odds is >= 0 and < 3)
            {
                sentence = await lipsum.GetBacon();
            }
            else if (odds is >= 3 and < 10)
            {
                sentence = await lipsum.GetMetaphorSentence();
            }
            else if (message.Text?.Split(' ').Length > 3)
            {
                sentence = MockFilter.Apply(message.Text);
            }
        
            if (!string.IsNullOrWhiteSpace(sentence))
            {
                await client.SendTextMessageAsync(chat, sentence, parseMode: ParseMode.Markdown, replyParameters: message);
            }
            
        }).WithPriority(Priority.Low + 1000);
    }
}