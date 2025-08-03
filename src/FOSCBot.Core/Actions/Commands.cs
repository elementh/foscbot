using Bottom;
using FOSCBot.Core.Helpers;
using FOSCBot.Infrastructure.Contract.Service;
using Incremental.Common.Random;
using Navigator.Abstractions.Client;
using Navigator.Actions.Builder.Extensions;
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
        catalog
            .OnCommand("about", async (INavigatorClient client, Chat chat) =>
            {
                const string about =
                    """
                    More info about FOSC at [fosc.space](https://fosc.space).
                    Check out my source code at [foscbot](https://github.com/elementh/foscbot).

                    Made with ❤️ and gratitude by [Lucas Maximiliano Marino](https://lucasmarino.me) & many others for all the cool people at [FOSC](https://fosc.space). 
                    """;

                await client.SendMessage(chat, about, parseMode: ParseMode.Markdown);
            })
            .WithChatAction(ChatAction.Typing);

        catalog
            .OnCommand("boi").SendRandomStickerFrom([
                "CAACAgQAAxkBAAI5Hl59wgU-EjcPoQJXOk81dz15rU6vAAIXAAMN0oYC8U1SU9k9QFQYBA",
                "CAACAgQAAxkBAAI5H159wgZdNShe0hTs65NLdinvP9v5AAIOAAMN0oYCfWiupsMYNyUYBA",
                "CAACAgQAAxkBAAI5Il59wgpjoo8hIuSmS7HRs44rneMRAAIeAAMN0oYCa2VzcOI0p0sYBA"
            ]);

        catalog
            .OnCommand("bottomify", async (INavigatorClient client, Chat chat, Message message) =>
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
                    return await client.SendMessage(chat, Bottomify.EncodeString(message.ReplyToMessage.Text),
                        replyParameters: message.ReplyToMessage);

                var input = message.Text?.Remove(0, message.Text.IndexOf(' ') + 1);

                if (!string.IsNullOrWhiteSpace(input)) return await client.SendMessage(chat, Bottomify.EncodeString(input));

                var randomText = Bottomify.EncodeString(lines[RandomProvider.GetThreadRandom()!.Next(0, lines.Length)]);

                return await client.SendMessage(chat, randomText);
            })
            .WithChatAction(ChatAction.Typing);

        catalog
            .OnCommand("coronashark")
            .SendSticker("CAACAgQAAxkBAAI4_l59L095Ep-xxos5f_7KBYkVlbu5AAKcBgACL9trAAF-dsaP9FZw_hgE");

        catalog
            .OnCommand("matalo",
                async (INavigatorClient client, Chat chat, Message message, IInsultService insults, IYesNoService yesNo) =>
                {
                    if (message.ReplyToMessage is null)
                    {
                        var answer = await yesNo.GetNoImage(CancellationToken.None);

                        if (!string.IsNullOrWhiteSpace(answer)) return await client.SendVideo(chat, answer, replyParameters: message);
                    }
                    else
                    {
                        var insult = await insults.GetInsult(CancellationToken.None);

                        if (!string.IsNullOrWhiteSpace(insult))
                            return await client.SendMessage(chat, insult, replyParameters: message);
                    }

                    return default;
                })
            .WithChatAction(ChatAction.Typing);

        catalog
            .OnCommand("nope")
            .SendVideo("https://raw.githubusercontent.com/elementh/foscbot/master/assets/nope.mp4");

        catalog
            .OnCommand("p4cock")
            .SendSticker("CAACAgQAAxkBAAMvXn0csAE-VH1a5YlL_C3y_uvmyhoAAk4DAAIv22sAAQYHFmm8oYuhGAQ", toReply: true);

        catalog
            .OnCommand("quote",
                async Task (INavigatorClient client, Chat chat, IInspiroService quotes) =>
                    await client.SendPhoto(chat, await quotes.GetInspiroImage()))
            .WithChatAction(ChatAction.UploadPhoto);

        catalog
            .OnCommand("raniilove")
            .SendSticker("CAACAgEAAxkBAAMyXn0ejAABhNQUUOtuxi41w8zpW1kbAAKNAAM4DoIRRihUBMGXYkoYBA");

        catalog
            .OnCommand("sad")
            .SendSticker("CAACAgQAAxkBAAI5DF59uqkJYnqzc5LcnEC_bdp0rerIAAJsAwACmOejAAG_qYNUT_L_exgE");

        catalog
            .OnCommand("start", async (INavigatorClient client, Chat chat) =>
            {
                const string start =
                    """
                    Hey there _newbie_. What is *FOSC*?

                    FOSC stands for `Free Open Source Club`. We are a student association focused on expanding free software and hacker culture.
                    """;

                await client.SendMessage(chat, start, parseMode: ParseMode.Markdown);
            })
            .WithChatAction(ChatAction.Typing);

        catalog
            .OnCommand("succ", async (INavigatorClient client, Chat chat, Message message) =>
            {
                var chance = RandomProvider.GetThreadRandom()!.NextDouble();

                var link = chance < 0.7d
                    ? "https://raw.githubusercontent.com/elementh/foscbot/master/assets/succ.mp4"
                    : "https://raw.githubusercontent.com/elementh/foscbot/master/assets/succ_with_teeth.mp4";

                await client.SendVideo(chat, link, replyParameters: message.ReplyToMessage ?? default(ReplyParameters));
            })
            .WithChatAction(ChatAction.UploadVideo);

        catalog
            .OnCommand("upct")
            .SendSticker("CAACAgQAAxkBAAJNW16eEHOauvBkLuaD-jL95s86vn2qAAJuAwACmOejAAEys6bCdTOD7RgE");

        catalog
            .OnCommand("want")
            .SendRandomStickerFrom([
                "CAACAgQAAxkBAAI5Yl59xpGz9uvLhFud46MfBOsOKAEZAAKRAAPXYpsOoD4HwY0npyEYBA",
                "CAACAgIAAxkBAAI5Y159xpQAAcud_5qj0XBWWtBYqzJ5OAACLVQAAp7OCwAB3ByKGvkbQr8YBA",
                "CAACAgIAAxkBAAI5Zl59xpcmSV8AAW8fzV2mOUkGCQeJCQACAwEAAladvQoC5dF4h-X6TxgE",
                "CAACAgQAAxkBAAI5aF59xp59Pnx0Y9_7iFLQwq56EP0jAAJ1FwAC_wRTAAGZhZMAARxrsr0YBA",
                "CAACAgEAAxkBAAI5al59xrCk3wAB13zrjcEKqtCJlnvpNwACDgcAAknjsAhFpPCKz57EtRgE"
            ]);
    }
}