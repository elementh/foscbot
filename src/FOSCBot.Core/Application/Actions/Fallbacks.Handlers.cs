using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Incremental.Common.Random;
using FOSCBot.Core.Application.Abstractions;
using FOSCBot.Core.Application.Services;
using FOSCBot.Core.Common;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Client;
using Navigator.Abstractions.Introspection;
using Navigator.Strategy;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Application.Actions;

public class CatchAllFallbackHandler;

public static partial class Fallbacks
{
    private static async Task UserTargetedFallbackHandler(INavigatorClient client, Chat chat, Message message,
        IUserFallbackService userFallbackService, ILogger<DefaultNavigationStrategy> logger,
        INavigatorTracerFactory<UserTargetedFallbackHandler> tracerFactory)
    {
        await using var tracer = tracerFactory.Get();

        try
        {
            var userId = message.From?.Id;

            if (userId is null)
            {
                MarkNotHandled(tracer, "no_user_id");
                return;
            }

            var fallback = await userFallbackService.GetAsync(userId.Value);

            if (fallback is null || fallback.Sentences.Count == 0)
            {
                MarkNotHandled(tracer, "no_user_config");
                return;
            }

            var roll = RandomProvider.GetThreadRandom()!.NextDouble();

            tracer.AddTag("fallback.user_targeted.odds", fallback.Odds.ToString());
            tracer.AddTag("fallback.user_targeted.roll", roll.ToString());

            if (roll >= fallback.Odds)
            {
                MarkNotHandled(tracer, "odds_missed");
                return;
            }

            var sentence = fallback.Sentences.ElementAt(RandomProvider.GetThreadRandom()!.Next(0, fallback.Sentences.Count)).Text;

            MarkHandled(tracer, "user_targeted", "odds_hit");
            await client.SendChatAction(chat, ChatAction.Typing);
            await Task.Delay(750);
            await client.SendMessage(chat, sentence, replyParameters: message, parseMode: ParseMode.Markdown);
        }
        catch (Exception e)
        {
            tracer.SetError(e);
            logger.LogError(e, "Failed to process user targeted fallback for chat {ChatId}", chat.Id);
        }
    }
    private const string SergioParadoxAudioUrl = "https://github.com/elementh/foscbot/raw/refs/heads/feature/social-credit-dystopia-phase-2/assets/audio/sergio-s-paradox.mp3";
    private const string SergioOpinionMeaning =
        """
        The message is asking FOSCBot what it thinks about Sergio or @linuxct.
        Count direct questions about Sergio or @linuxct.
        Also count self-referential questions like "what do you think about me fosbo?" only when the sender username is "linuxct".
        Do not count unrelated mentions, statements, or messages that are not asking for FOSCBot's opinion.
        """;

    // Word-bounded: bare Contains("work") also counted framework/network/coworker
    // and inflated the >= 3 keyword threshold from innocent tech talk.
    private static readonly string[] SergioParadoxKeywordPatterns =
        [@"\bgoogl\w*", @"\bsergio\b", @"\blinuxct\b", @"\bwork(?:s|ing|ed)?\b", @"\bjobs?\b", @"\btrabaj\w*"];

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

    private static readonly string[] SergioOpinionLines =
    [
        "{0} is a bit weird, but also suspiciously god-tier.",
        "{0} has the aura of a strange little prophet and the stats of a deity.",
        "{0} is definitely a bit weird, which is exactly what makes the godhood believable.",
        "{0} walks the thin line between oddball and divine intervention.",
        "{0} is kind of weird, yes, but in the same way ancient gods were weird.",
        "{0} feels like a man, a myth, and a mildly cursed saint all at once.",
        "{0} is a weird guy, but unfortunately for the rest of us, also a god.",
        "{0} radiates confusing energy and undeniable divinity in equal measure.",
        "I once had hot, steamy, digital... exchanges with him."
    ];

    private static readonly string[] SassLines =
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

    [Experimental("SKEXP0001")]
    private static async Task CatchAllHandler(INavigatorClient client, Chat chat, Message message, Update update,
        ProbabilityService probabilities, IAgentService agentService, ITextMeaningService textMeaningService, ILogger<DefaultNavigationStrategy> logger,
        INavigatorTracerFactory<CatchAllFallbackHandler> tracerFactory)
    {
        await using var tracer = tracerFactory.Get();

        try
        {
            if (message.Type is not (MessageType.Text or MessageType.Sticker))
            {
                MarkNotHandled(tracer, "unsupported_message_type");
                return;
            }

            var context = new CatchAllContext(
                chat,
                message,
                RandomProvider.GetThreadRandom()!,
                update.IsBotMentioned(),
                update.IsBotQuoted());

            if (await TryHandleSergioOpinionAsync(client, context, textMeaningService, tracer))
                return;

            if (await TryHandleGoogleChantAsync(client, context, tracer))
                return;

            if (await TryHandleSergioParadoxAsync(client, context, agentService, tracer))
                return;

            if (await TryHandleStacktraceCoronerAsync(client, context, agentService, tracer))
                return;

            if (await TryHandleThreadVerdictAsync(client, context, agentService, tracer))
                return;

            if (await TryHandleAgentReplyAsync(client, context, probabilities, agentService, tracer))
                return;

            if (await TryHandleQuotedSassAsync(client, context, tracer))
                return;

            MarkNotHandled(tracer, "no_match");
        }
        catch (Exception e)
        {
            tracer.SetError(e);
            logger.LogError(e, "Failed to process fallback handler for chat {ChatId}", chat.Id);
        }
    }

    private static async Task<bool> TryHandleSergioOpinionAsync(INavigatorClient client, CatchAllContext context,
        ITextMeaningService textMeaningService, INavigatorTracer tracer)
    {
        if (context.Message.Text is not { } opinionText || !CanBeSergioOpinionCandidate(context, opinionText))
            return false;

        var meaningMatches = await textMeaningService.MatchesMeaning(
            opinionText,
            SergioOpinionMeaning,
            new TextMeaningContext(context.Message.From?.Username, context.Message.From?.FirstName));

        if (!meaningMatches)
            return false;

        var sergioAlias = ResolveSergioOpinionAlias(context, opinionText);

        MarkHandled(tracer, "sergio_opinion", "sergio_opinion_question");
        await client.SendChatAction(context.Chat, ChatAction.Typing);
        await client.SendMessage(context.Chat,
            $"`{string.Format(SergioOpinionLines[context.Random.Next(0, SergioOpinionLines.Length)], sergioAlias)}`",
            parseMode: ParseMode.Markdown,
            replyParameters: context.Message);

        return true;
    }

    private static async Task<bool> TryHandleGoogleChantAsync(INavigatorClient client, CatchAllContext context, INavigatorTracer tracer)
    {
        if (context.Message.Text is not { } googleChantText || !IsGoogleChantTrigger(googleChantText))
            return false;

        MarkHandled(tracer, "google_chant", "chant_match");
        await client.SendChatAction(context.Chat, ChatAction.Typing);
        await client.SendMessage(context.Chat,
            GoogleChantLines[context.Random.Next(0, GoogleChantLines.Length)],
            parseMode: ParseMode.Html);

        return true;
    }

    [Experimental("SKEXP0001")]
    private static async Task<bool> TryHandleSergioParadoxAsync(INavigatorClient client, CatchAllContext context, IAgentService agentService,
        INavigatorTracer tracer)
    {
        if (!CanTriggerSergioParadox(context))
            return false;

        MarkHandled(tracer, "sergio_paradox", "sergio_keywords");

        switch (context.Random.Next(0, 3))
        {
            case 0:
                await HandleSergioSongAsync(client, context, tracer);
                break;
            case 1:
                await HandleSergioAgentCommentAsync(client, context, agentService, tracer);
                break;
            default:
                await HandleSergioStaticReplyAsync(client, context, tracer);
                break;
        }

        return true;
    }

    [Experimental("SKEXP0001")]
    private static async Task<bool> TryHandleStacktraceCoronerAsync(INavigatorClient client, CatchAllContext context,
        IAgentService agentService, INavigatorTracer tracer)
    {
        if (context.Message.Text is not { } text)
            return false;

        if (!LooksLikeDebugFailure(text))
            return false;

        MarkHandled(tracer, "stacktrace_coroner", "debug_failure");
        await client.SendChatAction(context.Chat, ChatAction.Typing);

        var username = context.Message.From?.Username ?? context.Message.From?.FirstName ?? "Anonymous";
        var response = await agentService.CoronerReply(text, username);

        if (string.IsNullOrWhiteSpace(response))
            return true;

        if (response.Length > 350)
            response = await agentService.ReduceTextLength(response, "350 characters") ?? response;

        await client.SendMessage(
            context.Chat,
            response,
            parseMode: ParseMode.Markdown,
            replyParameters: context.Message);

        return true;
    }

    [Experimental("SKEXP0001")]
    private static async Task<bool> TryHandleThreadVerdictAsync(INavigatorClient client, CatchAllContext context,
        IAgentService agentService, INavigatorTracer tracer)
    {
        if (context.Message.Text is not { } text)
            return false;

        if (!context.IsBotMentioned && !context.IsBotQuoted)
            return false;

        if (!LooksLikeVerdictRequest(text))
            return false;

        MarkHandled(tracer, "thread_verdict", "verdict_request");
        await client.SendChatAction(context.Chat, ChatAction.Typing);

        var response = await agentService.ThreadVerdict(context.Chat, context.Message);

        if (string.IsNullOrWhiteSpace(response))
            return true;

        if (response.Length > 350)
            response = await agentService.ReduceTextLength(response, "350 characters") ?? response;

        await client.SendMessage(
            context.Chat,
            response,
            parseMode: ParseMode.Markdown,
            replyParameters: context.Message);

        return true;
    }

    [Experimental("SKEXP0001")]
    private static async Task<bool> TryHandleAgentReplyAsync(INavigatorClient client, CatchAllContext context,
        ProbabilityService probabilities, IAgentService agentService, INavigatorTracer tracer)
    {
        if (!TryGetAgentReplyReason(context, probabilities, out var reason))
            return false;

        MarkHandled(tracer, "agent_reply", reason);
        await client.SendChatAction(context.Chat, ChatAction.Typing);

        var response = await agentService.ProcessMessage(context.Chat, context.Message);
        var reduced = false;

        if (response?.Length > 200)
        {
            response = await agentService.ReduceTextLength(response, "200 characters");
            reduced = true;
        }

        tracer.AddTag("fallback.agent.reduced", reduced ? "True" : "False");
        tracer.AddTag("fallback.agent.response_null", response is null ? "True" : "False");

        if (response == null)
            return true;

        await client.SendMessage(context.Chat.Id, response, parseMode: ParseMode.Markdown);
        probabilities.Reset(context.Chat.Id);

        return true;
    }

    private static async Task<bool> TryHandleQuotedSassAsync(INavigatorClient client, CatchAllContext context, INavigatorTracer tracer)
    {
        if (!context.IsBotQuoted)
            return false;

        MarkHandled(tracer, "quoted_sass", "bot_quoted");
        await client.SendChatAction(context.Chat, ChatAction.Typing);
        await client.SendMessage(context.Chat,
            SassLines[context.Random.Next(0, SassLines.Length)],
            parseMode: ParseMode.Markdown,
            replyParameters: context.Message);

        return true;
    }

    private static bool CanTriggerSergioParadox(CatchAllContext context)
    {
        return context.Message.Text is { } messageText
               && !context.IsBotQuoted
               && IsSergioParadoxTrigger(messageText);
    }

    private static bool TryGetAgentReplyReason(CatchAllContext context, ProbabilityService probabilities, out string reason)
    {
        if (context.IsBotMentioned)
        {
            reason = "bot_mentioned";
            return true;
        }

        var shouldReplyByProbability = probabilities.GetResult(context.Chat.Id);
        var shouldReplyToQuote = context.IsBotQuoted && context.Random.NextDouble() > 0.3d;

        if (shouldReplyByProbability)
        {
            reason = "probability";
            return true;
        }

        if (shouldReplyToQuote)
        {
            reason = "bot_quoted";
            return true;
        }

        reason = string.Empty;
        return false;
    }

    private static async Task HandleSergioSongAsync(INavigatorClient client, CatchAllContext context, INavigatorTracer tracer)
    {
        tracer.AddTag("fallback.sergio.mode", "song");
        await client.SendChatAction(context.Chat, ChatAction.Typing);
        await client.SendMessage(context.Chat,
            $"`{SergioParadoxSongIntroLines[context.Random.Next(0, SergioParadoxSongIntroLines.Length)]}`",
            parseMode: ParseMode.Markdown);
        await client.SendChatAction(context.Chat, ChatAction.UploadVoice);

        using var httpClient = new HttpClient();
        await using var audioStream = await httpClient.GetStreamAsync(SergioParadoxAudioUrl);
        await client.SendVoice(context.Chat, new InputFileStream(audioStream, "sergio-s-paradox.mp3"));
    }

    [Experimental("SKEXP0001")]
    private static async Task HandleSergioAgentCommentAsync(INavigatorClient client, CatchAllContext context, IAgentService agentService,
        INavigatorTracer tracer)
    {
        tracer.AddTag("fallback.sergio.mode", "agent_comment");
        await client.SendChatAction(context.Chat, ChatAction.Typing);

        var username = context.Message.From?.Username ?? context.Message.From?.FirstName ?? "Anonymous";
        var response = await agentService.CommentOnSergioParadox(context.Message.Text!, username);

        if (!string.IsNullOrWhiteSpace(response))
            await client.SendMessage(context.Chat.Id, response);
    }

    private static async Task HandleSergioStaticReplyAsync(INavigatorClient client, CatchAllContext context, INavigatorTracer tracer)
    {
        tracer.AddTag("fallback.sergio.mode", "static_reply");
        await client.SendChatAction(context.Chat, ChatAction.Typing);
        await client.SendMessage(context.Chat,
            $"`{SergioParadoxLines[context.Random.Next(0, SergioParadoxLines.Length)]}`",
            parseMode: ParseMode.Markdown);
    }

    private static void MarkHandled(INavigatorTracer tracer, string fallbackName, string reason)
    {
        tracer.AddTag("FallbackTriggered", "True");
        tracer.AddTag("fallback.name", fallbackName);
        tracer.AddTag("fallback.reason", reason);
    }

    private static void MarkNotHandled(INavigatorTracer tracer, string reason)
    {
        tracer.AddTag("FallbackTriggered", "False");
        tracer.AddTag("fallback.name", "none");
        tracer.AddTag("fallback.reason", reason);
    }

    private static bool IsSergioParadoxTrigger(string text)
    {
        var matches = SergioParadoxKeywordPatterns.Count(pattern =>
            Regex.IsMatch(text, pattern, RegexOptions.IgnoreCase));

        return matches >= 3;
    }

    private static bool CanBeSergioOpinionCandidate(CatchAllContext context, string text)
    {
        if (!ContainsFosboReference(text))
            return false;

        return IsSergioMention(text) ||
               (string.Equals(context.Message.From?.Username, "linuxct", StringComparison.InvariantCultureIgnoreCase) &&
                LooksLikeOpinionQuestion(text));
    }

    private static string ResolveSergioOpinionAlias(CatchAllContext context, string text)
    {
        if (text.Contains("linuxct", StringComparison.InvariantCultureIgnoreCase) ||
            string.Equals(context.Message.From?.Username, "linuxct", StringComparison.InvariantCultureIgnoreCase))
            return "@linuxct";

        return "Sergio";
    }

    private static bool ContainsFosboReference(string text)
    {
        return text.Contains("fosbo", StringComparison.InvariantCultureIgnoreCase) ||
               text.Contains("foscbot", StringComparison.InvariantCultureIgnoreCase);
    }

    private static bool IsSergioMention(string text)
    {
        return
            text.Contains("sergio", StringComparison.InvariantCultureIgnoreCase) ||
            text.Contains("linuxct", StringComparison.InvariantCultureIgnoreCase);
    }

    private static bool LooksLikeOpinionQuestion(string text)
    {
        return text.Contains('?', StringComparison.InvariantCultureIgnoreCase) ||
               text.Contains("think", StringComparison.InvariantCultureIgnoreCase) ||
               text.Contains("opinion", StringComparison.InvariantCultureIgnoreCase) ||
               text.Contains("piens", StringComparison.InvariantCultureIgnoreCase) ||
               text.Contains("parece", StringComparison.InvariantCultureIgnoreCase);
    }

    private static bool IsGoogleChantTrigger(string text)
    {
        // WORK FOR GOOGLE must stay case-sensitive: the chant is shouted, while
        // lowercase "maybe I should work for Google" is an earnest career sentence.
        return text.Contains("GOO GOO GLE", StringComparison.InvariantCultureIgnoreCase)
               || text.Contains("GOO GOO GOO", StringComparison.InvariantCultureIgnoreCase)
               || text.Contains("WORK FOR GOOGLE", StringComparison.Ordinal);
    }

    private static bool LooksLikeDebugFailure(string text)
    {
        // "exception" needs error context ("I'll make an exception this time" is chat,
        // not a crash), "error:"/"fatal:" must start a line like real tool output, and
        // "at "-lines must look like stack frames (schedules also start lines with "at").
        if (text.Contains("traceback", StringComparison.InvariantCultureIgnoreCase)) return true;
        if (ExceptionContextRegex().IsMatch(text)) return true;
        if (text.Contains("stack trace", StringComparison.InvariantCultureIgnoreCase)) return true;
        if (ErrorLineRegex().IsMatch(text)) return true;
        if (text.Contains("segmentation fault", StringComparison.InvariantCultureIgnoreCase)) return true;
        if (text.Contains("build failed", StringComparison.InvariantCultureIgnoreCase)) return true;
        if (text.Contains("failed with exit code", StringComparison.InvariantCultureIgnoreCase)) return true;
        if (text.Contains("panic:", StringComparison.InvariantCultureIgnoreCase)) return true;

        var atCount = text.Split('\n').Count(line => StackFrameLineRegex().IsMatch(line));

        return atCount >= 2;
    }

    private static bool LooksLikeVerdictRequest(string text)
    {
        // Regexes tolerate smart apostrophes (who’s) and mixed accents (quien tiene razón);
        // "your take" replaces bare "take?", which matched any "how long will it take?".
        return WhoIsRightRegex().IsMatch(text)
               || QuienTieneRazonRegex().IsMatch(text)
               || text.Contains("settle this", StringComparison.InvariantCultureIgnoreCase)
               || text.Contains("veredicto", StringComparison.InvariantCultureIgnoreCase)
               || text.Contains("your take", StringComparison.InvariantCultureIgnoreCase)
               || HotTakeRegex().IsMatch(text)
               || text.Contains("opinion on this thread", StringComparison.InvariantCultureIgnoreCase)
               || text.Contains("opinion please", StringComparison.InvariantCultureIgnoreCase);
    }

    [GeneratedRegex(
        @"\b[A-Z]\w+(?:Exception|Error)\b|\bexception\b\s*[:(]|\b(?:unhandled|uncaught|threw|throws|throwing|caught|raised)\s+(?:an?\s+)?exception\b|\bexception\s+(?:was\s+)?(?:thrown|raised|occurred)\b",
        RegexOptions.IgnoreCase)]
    private static partial Regex ExceptionContextRegex();

    [GeneratedRegex(@"^\s*(?:error|fatal)\s*:", RegexOptions.IgnoreCase | RegexOptions.Multiline)]
    private static partial Regex ErrorLineRegex();

    [GeneratedRegex(@"^\s*at\s+\S+.*\(")]
    private static partial Regex StackFrameLineRegex();

    [GeneratedRegex(@"who\s*['’]?s right|who (?:is|was) right", RegexOptions.IgnoreCase)]
    private static partial Regex WhoIsRightRegex();

    [GeneratedRegex(@"qui[eé]n tiene raz[oó]n", RegexOptions.IgnoreCase)]
    private static partial Regex QuienTieneRazonRegex();

    [GeneratedRegex(@"\bhot take\b", RegexOptions.IgnoreCase)]
    private static partial Regex HotTakeRegex();

    private readonly record struct CatchAllContext(
        Chat Chat,
        Message Message,
        Random Random,
        bool IsBotMentioned,
        bool IsBotQuoted);
}
