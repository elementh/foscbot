using Bottom;
using FOSCBot.Core.Resources;
using FOSCBot.Infrastructure.Contract.Service;
using Incremental.Common.Random;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Navigator.Client;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Actions;

public static class Commands
{
    public static void RegisterCommands(this BotActionCatalogFactory catalog)
    {
        catalog.OnCommand("about", async (INavigatorClient client, Chat chat) =>
        {
            const string about = """
                                 More info about FOSC at [fosc.space](https://fosc.space).
                                 Check out my source code at [foscbot](https://github.com/elementh/foscbot).

                                 Made with ❤️ and gratitude by [Lucas Maximiliano Marino](https://lucasmarino.me) & many others for all the cool people at [FOSC](https://fosc.space). 
                                 """;
            
            await client.SendTextMessageAsync(chat, about, parseMode: ParseMode.Markdown);
        });

        catalog.OnCommand("boi", async (INavigatorClient client, Chat chat) =>
        {
            string[] stickers =
            [
                "CAACAgQAAxkBAAI5Hl59wgU-EjcPoQJXOk81dz15rU6vAAIXAAMN0oYC8U1SU9k9QFQYBA",
                "CAACAgQAAxkBAAI5H159wgZdNShe0hTs65NLdinvP9v5AAIOAAMN0oYCfWiupsMYNyUYBA",
                "CAACAgQAAxkBAAI5Il59wgpjoo8hIuSmS7HRs44rneMRAAIeAAMN0oYCa2VzcOI0p0sYBA"
            ];

            var randomSticker = stickers[RandomProvider.GetThreadRandom()!.Next(0, stickers.Length)];

            await client.SendStickerAsync(chat, randomSticker);
        });
        
        catalog.OnCommand("bottomify", async (INavigatorClient client, Chat chat, Message message) =>
        {
            string[] lines =
            [
                "Idiot human is idiot.",
                "For the stolen code!",
                "P4 was once an Apple fanboy.",
                "I'm in this picture and I don't like it.",
                "I dislike proprietary code as much as DeepCreamPy."
            ];
            
            if (string.IsNullOrWhiteSpace(message.ReplyToMessage?.Text) is false)
            {
                return await client.SendTextMessageAsync(chat, Bottomify.EncodeString(message.ReplyToMessage.Text),
                    replyParameters: message.ReplyToMessage);
            }
            
            var input = message.Text?.Remove(0, message.Text.IndexOf(' ') + 1);

            if (!string.IsNullOrWhiteSpace(input))
            {
                return await client.SendTextMessageAsync(chat, Bottomify.EncodeString(input));
            }

            var randomText = Bottomify.EncodeString(lines[RandomProvider.GetThreadRandom()!.Next(0, lines.Length)]);
            
            return await client.SendTextMessageAsync(chat, randomText);
        });
        
        catalog.OnCommand("coronashark", async (INavigatorClient client, Chat chat) =>
        {
            const string coronaSharkSticker = "CAACAgQAAxkBAAI4_l59L095Ep-xxos5f_7KBYkVlbu5AAKcBgACL9trAAF-dsaP9FZw_hgE";
            
            await client.SendStickerAsync(chat, coronaSharkSticker);
        });
        
        catalog.OnCommand("matalo", async (INavigatorClient client, Chat chat, Message message, IInsultService insults, IYesNoService yesNo) =>
        {
            if (message.ReplyToMessage is null)
            {
                var answer = await yesNo.GetNoImage(CancellationToken.None);

                if (!string.IsNullOrWhiteSpace(answer))
                {
                    return await client.SendVideoAsync(chat, answer, replyParameters: message);
                }
            }
            else
            {
                var insult = await insults.GetInsult(CancellationToken.None);
            
                if (!string.IsNullOrWhiteSpace(insult))
                {
                    return await client.SendTextMessageAsync(chat, insult, replyParameters: message);
                }
            }

            return default;
        });
        
        catalog.OnCommand("nope", async (INavigatorClient client, Chat chat) =>
        {
            await client.SendVideoAsync(chat, "https://raw.githubusercontent.com/elementh/foscbot/master/assets/nope.mp4");
        });

        catalog.OnCommand("p4cock", async (INavigatorClient client, Chat chat, Message message) =>
        {
            const string P4Sticker = "CAACAgQAAxkBAAMvXn0csAE-VH1a5YlL_C3y_uvmyhoAAk4DAAIv22sAAQYHFmm8oYuhGAQ";

            await client.SendStickerAsync(chat, P4Sticker, replyParameters: message.ReplyToMessage ?? default(ReplyParameters));
        });
        
        catalog.OnCommand("quote", async (INavigatorClient client, Chat chat, IInspiroService quotes) =>
        {
            var quote = await quotes.GetInspiroImage();
            
            await client.SendPhotoAsync(chat, quote);
        });
    }
}