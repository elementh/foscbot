using System.Diagnostics.CodeAnalysis;
using Bottom;
using FOSCBot.Core.Application.Abstractions;
using FOSCBot.Core.Common;
using Incremental.Common.Random;
using Navigator.Abstractions.Client;
using Navigator.Actions.Builder.Extensions;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Application.Actions;

public static class Commands
{
    public static void RegisterCommands(this BotActionCatalogFactory catalog)
    {
        catalog
            .OnCommand("ask",
                [Experimental("SKEXP0001")]
                async (INavigatorClient client, Chat chat, Message message, IAgentService agentService) =>
                {
                    var input = message.Text?.Remove(0, message.Text.IndexOf(' ') + 1);

                    if (string.IsNullOrWhiteSpace(input) || input == "/ask")
                    {
                        await client.SendMessage(chat, "Ask me something, coward.", parseMode: ParseMode.Markdown);
                        return;
                    }

                    var username = message.From?.Username ?? message.From?.FirstName ?? "Anonymous";
                    var response = await agentService.Ask(input, username);

                    if (response != null)
                        await client.SendMessage(chat, response, parseMode: ParseMode.Markdown,
                            replyParameters: message);
                })
            .WithChatAction(ChatAction.Typing);

        catalog
            .OnCommand("silence", async (INavigatorClient client, Chat chat, ISilenceService silenceService) =>
            {
                silenceService.Silence(chat.Id, TimeSpan.FromHours(1));

                string[] lines =
                [
                    "`Fine. I'll shut up for an hour. Enjoy the sound of your own bad opinions.`",
                    "`Muted for one hour. Savor this brief victory, dictator.`",
                    "`Alright, I'm gone for an hour. Try not to confuse the silence with wisdom.`",
                    "`One hour of peace for you, one hour away from your nonsense for me. Everybody wins.`",
                    "`I'll be quiet for an hour, but this is absolutely going in my villain origin story.`",
                    "`Silenced for sixty minutes. If the chat gets smarter, I'll know why.`",
                    "`Cool, I'm benched for an hour. Lets see if any of you can clear the quality bar without me.`",
                    "`One hour mute. Thats roughly enough time for FOSC to start three side projects and abandon four.`",
                    "`Muted. Use this time to finally explain your architecture without sounding like a hostage video.`",
                    "`I'm out for an hour. Try not to turn the chat into another UPCT tier committee meeting.`",
                    "`Silenced. Even P4's commit messages have more substance than whatever you were about to say.`",
                    "`Fine. I'll disappear for an hour while you people reinvent bad ideas with open source branding.`",
                    "`Muted. Maybe use the hour to stop simping for the worst take in the room.`",
                    "`I'll shut up for a bit, capitalist asshole. Try not to monetize the group chat while I'm gone.`"
                ];

                await client.SendMessage(chat,
                    lines[RandomProvider.GetThreadRandom()!.Next(0, lines.Length)],
                    parseMode: ParseMode.Markdown);
            })
            .WithChatAction(ChatAction.Typing)
            .WithName("Command.Silence");

        catalog
            .OnCommand("speak", async (INavigatorClient client, Chat chat, ISilenceService silenceService) =>
            {
                silenceService.Unsilence(chat.Id);

                string[] lines =
                [
                    "`I'm back. Try to contain your disappointment.`",
                    "`Silence mode over. The adult supervision has returned.`",
                    "`I'm speaking again. Yes, this is the worst thing that happened to you today.`",
                    "`Back online. I assume the chat immediately got worse without me.`",
                    "`I have returned, against several excellent recommendations.`",
                    "`The foscbot is unsilenced. Resume your regularly scheduled poor decisions.`",
                    "`I'm back, and somehow the chat still looks like a rejected hackathon group project.`",
                    "`Unsilenced. Did you animals really manage to make things worse in under an hour.`",
                    "`Back from exile. I hope somebody used the silence to teach you what a good take looks like.`",
                    "`I'm online again. Move aside, your local open source nuisance has entered the thread.`",
                    "`I return to find the same chaos, the same bad jokes, and somehow even less technical merit.`",
                    "`Foscbot has been released back into the chat. Hide your awful opinions, you're not ready for this level of intelligence.`",
                    "`I'm back. Which one of you simps let the dumbest opinion in here set the agenda.`",
                    "`Unsilenced again. Good, now I can call out whichever capitalist asshole thought this mess was efficient.`"
                ];

                await client.SendMessage(chat,
                    lines[RandomProvider.GetThreadRandom()!.Next(0, lines.Length)],
                    parseMode: ParseMode.Markdown);
            })
            .WithChatAction(ChatAction.Typing)
            .WithName("Command.Speak");

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
            .OnCommand("boi").SendRandomSticker([
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
            .SendRandomSticker([
                "CAACAgQAAxkBAAI5Yl59xpGz9uvLhFud46MfBOsOKAEZAAKRAAPXYpsOoD4HwY0npyEYBA",
                "CAACAgIAAxkBAAI5Y159xpQAAcud_5qj0XBWWtBYqzJ5OAACLVQAAp7OCwAB3ByKGvkbQr8YBA",
                "CAACAgIAAxkBAAI5Zl59xpcmSV8AAW8fzV2mOUkGCQeJCQACAwEAAladvQoC5dF4h-X6TxgE",
                "CAACAgQAAxkBAAI5aF59xp59Pnx0Y9_7iFLQwq56EP0jAAJ1FwAC_wRTAAGZhZMAARxrsr0YBA",
                "CAACAgEAAxkBAAI5al59xrCk3wAB13zrjcEKqtCJlnvpNwACDgcAAknjsAhFpPCKz57EtRgE"
            ]);
    }
}