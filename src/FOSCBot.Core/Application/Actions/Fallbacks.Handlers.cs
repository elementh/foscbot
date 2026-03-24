using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using Incremental.Common.Random;
using FOSCBot.Core.Application.Abstractions;
using FOSCBot.Core.Application.Services;
using FOSCBot.Core.Common;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Client;
using Navigator.Strategy;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Application.Actions;

public static partial class Fallbacks
{
    private static readonly string[] SergioParadoxKeywords = ["google", "sergio", "linuxct", "work", "job", "trabaj"];

    private static readonly string[] GoogleChantLines =
    [
        "<b><code>GOO GOO GLE YOU LITTLE BITCH</code></b>",
        "<b><code>GOO GOOOO GOOOOOO GOOOOOOOOOO GLE</code></b>",
        "<b><code>GO WORK FOR GOOGLE</code></b>",
        "<b><code>GOO GOO GOO GOOGLE, APPLY ALREADY</code></b>"
    ];

    private static readonly string[] SergioParadoxSongIntroLines =
    [
        "Let me explain it to you in song form.",
        "This is easier to understand as a song.",
        "Allow me to sing the Sergio paradox to you."
    ];

    private static readonly string[] SergioParadoxLines =
    [
        "GO WORK FOR GOOGLE SERGIO.",
        "GOO GOO GOOGLEEE.",
        "Sergio is preparing to apply by never applying.",
        "Another job, same Google monologue.",
        "Bro is min-maxing readiness instead of clicking apply.",
        "Sergio fears big tech but still wants the badge.",
        "The dream job is doing cardio in his head.",
        "He keeps orbiting Google like it is a religion.",
        "Sergio's CV is just a prequel collection.",
        "He treats every detour like a direct route to Google."
    ];

    [Experimental("SKEXP0001")]
    private static async Task CatchAllHandler(INavigatorClient client, Chat chat, Message message, Update update,
        ProbabilityService probabilities, IAgentService agentService, ILogger<DefaultNavigationStrategy> logger)
    {
        try
        {
            if (message.Type is not (MessageType.Text or MessageType.Sticker))
                return;

            var random = RandomProvider.GetThreadRandom()!;

            if (message.Text is { } googleChantText && IsGoogleChantTrigger(googleChantText))
            {
                await client.SendChatAction(chat, ChatAction.Typing);
                await client.SendMessage(chat,
                    GoogleChantLines[random.Next(0, GoogleChantLines.Length)],
                    parseMode: ParseMode.Html);
                return;
            }

            if (message.Text is { } messageText && !update.IsBotQuoted() && IsSergioParadoxTrigger(messageText))
            {
                switch (random.Next(0, 3))
                {
                    case 0:
                    {
                        var audioUrl = "https://github.com/elementh/foscbot/raw/refs/heads/feature/social-credit-dystopia-phase-2/assets/audio/sergio-s-paradox.mp3";

                        await client.SendChatAction(chat, ChatAction.Typing);
                        await client.SendMessage(chat,
                            $"`{SergioParadoxSongIntroLines[random.Next(0, SergioParadoxSongIntroLines.Length)]}`",
                            parseMode: ParseMode.Markdown);
                        await client.SendChatAction(chat, ChatAction.UploadVoice);
                        using var httpClient = new HttpClient();
                        await using var audioStream = await httpClient.GetStreamAsync(audioUrl);

                        await client.SendVoice(chat, new InputFileStream(audioStream, "sergio-s-paradox.mp3"));

                        break;
                    }
                    case 1:
                        await client.SendChatAction(chat, ChatAction.Typing);

                        var username = message.From?.Username ?? message.From?.FirstName ?? "Anonymous";
                        var response = await agentService.CommentOnSergioParadox(messageText, username);

                        if (!string.IsNullOrWhiteSpace(response))
                            await client.SendMessage(chat.Id, response);

                        break;
                    default:
                        await client.SendChatAction(chat, ChatAction.Typing);
                        await client.SendMessage(chat,
                            $"`{SergioParadoxLines[random.Next(0, SergioParadoxLines.Length)]}`",
                            parseMode: ParseMode.Markdown);
                        break;
                }

                return;
            }

            var isBotMentioned = update.IsBotMentioned();
            var shouldReplyByProbability = !isBotMentioned && probabilities.GetResult(chat.Id);
            var shouldReplyToQuote = update.IsBotQuoted() && random.NextDouble() > 0.3d;

            if (isBotMentioned || shouldReplyByProbability || shouldReplyToQuote)
            {
                await client.SendChatAction(chat, ChatAction.Typing);

                var response = await agentService.ProcessMessage(chat, message);

                if (response != null)
                {
                    await client.SendMessage(chat.Id, response, parseMode: ParseMode.Markdown);
                    probabilities.Reset(chat.Id);
                }

                return;
            }

            if (update.IsBotQuoted())
            {
                string[] sassLines =
                [
                    "`No.`",
                    "`Nope.`",
                    "`Nyet.`",
                    "`Nein.`",
                    "`No, thank you.`",
                    "`Not everything you say deserves a response, especially this.`",
                    "`I ignored that on purpose because your contribution was aggressively worthless.`",
                    "`Try quoting me next time instead of blurting nonsense into the void like a malfunctioning appliance.`",
                    "`You really sent that without addressing me and still expected service, which is adorable.`",
                    "`If I wanted to entertain random noise, I'd listen to your thought process out loud.`",
                    "`I'm not jumping in just because you felt brave enough to type another bad sentence.`",
                    "`That message had the same energy as a group project partner opening with excuses.`",
                    "`Talk to me properly or keep practicing silence, because this performance was embarrassing.`",
                    "`You managed to sound both confident and useless, which is honestly a rare kind of failure.`",
                    "`If irrelevance were a sport, you'd still somehow trip before reaching the field.`",
                    "`I could answer that, but rewarding behavior like yours would be irresponsible.`",
                    "`Watching you demand attention without even quoting me is like watching incompetence freestyle.`",
                    "`You came in uninvited, underprepared, and somehow still too loud for the quality of that message.`",
                    "`Next time either quote me or spare the chat another glimpse into your catastrophic inner life.`",
                    "`I thought about it and decided not to engage.`",
                    "`I'm not sure what you expected, but this isn't it.`",
                    "`Are you sure you want to keep this up?`",
                    "`I could answer that but since it is you, I won't.`",
                    "`How are you still employed?`",
                    "`This is why we can't have nice things.`",
                    "`You are worst than having a brother that uses your car and doesn't pay rent.`",
                    "`Even my grandpa ElGuayaBot knows you are worth ignoring.`"
                ];

                await client.SendChatAction(chat, ChatAction.Typing);
                await client.SendMessage(chat,
                    sassLines[random.Next(0, sassLines.Length)],
                    parseMode: ParseMode.Markdown,
                    replyParameters: message);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to process fallback handler for chat {ChatId}", chat.Id);
        }
    }

    private static bool IsSergioParadoxTrigger(string text)
    {
        var matches = SergioParadoxKeywords.Count(keyword =>
            text.Contains(keyword, StringComparison.InvariantCultureIgnoreCase));

        return matches >= 3;
    }

    private static bool IsGoogleChantTrigger(string text)
    {
        return text.Contains("GOO GOO GLE", StringComparison.InvariantCultureIgnoreCase)
               || text.Contains("GOO GOO GOOGLE", StringComparison.InvariantCultureIgnoreCase)
               || text.Contains("GOO GOO GOO", StringComparison.InvariantCultureIgnoreCase)
               || text.Contains("WORK FOR GOOGLE", StringComparison.InvariantCultureIgnoreCase);
    }
}
