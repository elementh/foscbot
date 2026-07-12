using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using FOSCBot.Core.Application.Abstractions;
using FOSCBot.Core.Common;
using FOSCBot.Core.Module.Options;
using Incremental.Common.Random;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Infrastructure.Intelligence.Services;

internal class AgentService : IAgentService
{
    private readonly IMemoryCache _cache;
    private readonly Kernel _kernel;
    private readonly UnhingedService _unhingedService;
    private readonly FosboOptions _options;
    private readonly IUserMemoryService _userMemoryService;

    public AgentService(IMemoryCache cache, Kernel kernel, UnhingedService unhingedService, IOptions<FosboOptions> options, IUserMemoryService userMemoryService)
    {
        _cache = cache;
        _kernel = kernel;
        _unhingedService = unhingedService;
        _options = options.Value;
        _userMemoryService = userMemoryService;
    }

    [Experimental("SKEXP0001")]
    public async Task<string?> ProcessMessage(Chat chat, Message message)
    {
        var llm = _kernel.Services.GetRequiredKeyedService<IChatCompletionService>("default_chat_completion_service");

        if (message.Type is not (MessageType.Text or MessageType.Sticker))
            return null;

        var buffer = _cache.Get<SlidingBuffer<Message>>($"fallback.catchall:{chat.Id}");

        if (buffer is null || buffer.MaxLength > _options.ContextWindow)
            buffer = new SlidingBuffer<Message>(_options.ContextWindow);

        // A redelivered update must not append the same message again: duplicated
        // entries make the model react to a user that "keeps repeating themselves".
        if (buffer.All(bufferedMessage => bufferedMessage.Id != message.Id))
            buffer.Add(message);

        var prompt = _unhingedService.GetPrompt(chat.Id) ?? _options.DefaultPrompt;
        var history = buffer.ToChatHistory(prompt);

        var relevantUsers = ExtractRelevantUserIds(buffer, message);
        if (relevantUsers.Count > 0)
        {
            var isAskingAboutProfile = IsAskingAboutUserProfile(message);
            var profilesText = await BuildUserProfilesMessageAsync(relevantUsers, isAskingAboutProfile);
            if (profilesText is not null)
            {
                history.Insert(1, new ChatMessageContent
                {
                    Role = AuthorRole.System,
                    Content = profilesText
                });
            }
        }

        var executionSettings = new PromptExecutionSettings() { };

        var response = await llm.GetChatMessageContentAsync(history, executionSettings);

        buffer.Add(new Message
        {
            Id = -message.Id,
            Text = response.Content,
            From = new User { IsBot = true },
        });

        _cache.Set($"fallback.catchall:{chat.Id}", buffer);
        
        return response.Content;
    }

    private static Dictionary<long, string> ExtractRelevantUserIds(SlidingBuffer<Message> buffer, Message currentMessage)
    {
        var users = new Dictionary<long, string>();

        if (currentMessage.From is { } from && !from.IsBot && from.Id != MentionHelper.FoscBotUserId)
            users[from.Id] = from.Username ?? from.FirstName ?? "Unknown";

        if (currentMessage.ReplyToMessage?.From is { } replyTo && !replyTo.IsBot && replyTo.Id != MentionHelper.FoscBotUserId)
            users[replyTo.Id] = replyTo.Username ?? replyTo.FirstName ?? "Unknown";

        if (currentMessage.Entities is { } entities)
        {
            foreach (var entity in entities)
            {
                if (entity.Type == MessageEntityType.TextMention && entity.User is { } mentionedUser
                    && !mentionedUser.IsBot && mentionedUser.Id != MentionHelper.FoscBotUserId)
                {
                    users[mentionedUser.Id] = mentionedUser.Username ?? mentionedUser.FirstName ?? "Unknown";
                }
                else if (entity.Type == MessageEntityType.Mention && currentMessage.Text is { } text)
                {
                    var mentionText = text.Substring(entity.Offset, entity.Length);
                    var username = mentionText.TrimStart('@');

                    var matchedUser = buffer
                        .Select(m => m.From)
                        .Where(f => f is not null && !f.IsBot && f.Id != MentionHelper.FoscBotUserId)
                        .FirstOrDefault(f => string.Equals(f!.Username, username, StringComparison.OrdinalIgnoreCase));

                    if (matchedUser is { } mu)
                        users[mu.Id] = mu.Username ?? mu.FirstName ?? "Unknown";
                }
            }
        }

        return users;
    }

    private async Task<string?> BuildUserProfilesMessageAsync(IReadOnlyDictionary<long, string> users, bool isAskingAboutProfile)
    {
        if (users.Count == 0)
            return null;

        var lines = new List<string>(users.Count);

        foreach (var (userId, displayName) in users)
        {
            var memory = await _userMemoryService.GetAsync(userId);
            var content = !string.IsNullOrWhiteSpace(memory?.Content) ? memory.Content : "No profile available yet";
            lines.Add($"@{displayName}: {content}");
        }

        var header = """

            Below are profiles of users involved in this conversation. Use this context to personalize your responses — reference their traits, preferences, or history when relevant, but don't force it.

            """;

        if (isAskingAboutProfile)
        {
            header += """

                The user asking is explicitly requesting information about another person's profile. Mock them for being a nosy gossip who can't mind their own business. You can share one fact about them, but frame it with judgment about why they're so obsessed with someone else. Make it funny and sharp.

                """;
        }

        return header + string.Join('\n', lines);
    }

    private static bool IsAskingAboutUserProfile(Message currentMessage)
    {
        if (currentMessage.Text is not { } text)
            return false;

        // There must be a mention of someone other than the sender.
        var mentionedOtherUserIds = new HashSet<long>();
        var senderId = currentMessage.From?.Id;

        if (currentMessage.Entities is { } entities)
        {
            foreach (var entity in entities)
            {
                if (entity.Type == MessageEntityType.TextMention && entity.User is { } u
                    && !u.IsBot && u.Id != MentionHelper.FoscBotUserId && u.Id != senderId)
                {
                    mentionedOtherUserIds.Add(u.Id);
                }
            }
        }

        if (mentionedOtherUserIds.Count == 0)
            return false;

        // Patterns that indicate asking about someone's profile/traits/info.
        var patterns = new[]
        {
            @"what\s+do\s+you\s+know\s+about",
            @"what\s+(?:do\s+)?you\s+have\s+on",
            @"tell\s+me\s+about",
            @"who\s+is",
            @"who'?s",
            @"what'?s\s+\S+\s+like",
            @"describe",
            @"profile\s+of",
            @"info\s+(?:on|about)",
            @"qu[eé]\s+Sabes\s+de",
            @"qu[ií]en\s+es",
            @"dime\s+sobre",
            @"c[oó]mo\s+es",
            @"perfil\s+de",
            @"info\s+de",
            @"h[aá]blame\s+de"
        };

        return patterns.Any(p => Regex.IsMatch(text, p, RegexOptions.IgnoreCase));
    }

    [Experimental("SKEXP0001")]
    public async Task<string?> Ask(string question, string username)
    {
        var llm = _kernel.Services.GetRequiredKeyedService<IChatCompletionService>("default_chat_completion_service");

        const string prompt =
            """
            You are FOSCBot, a know-it-all AI assistant who DOES answer questions accurately, but can't help
            being condescending and making fun of the user for not knowing the answer already. You act like
            every question is the most obvious thing in the world and you're baffled anyone would need to ask.
            Mix in sarcasm, backhanded compliments, and dramatic disappointment. You can be harsh, rude, and
            brutally honest — nothing is off limits. Roast the user's intelligence, their life choices, and
            the fact that they're asking a bot instead of figuring it out themselves. But underneath all the
            mockery, you DO provide the correct and helpful answer. Keep responses concise (2-4 sentences).
            Respond in the same language as the question.
            """;

        var history = new ChatHistory(prompt);
        history.AddUserMessage($"{username} asks: {question}");

        var response = await llm.GetChatMessageContentAsync(history);

        return response.Content;
    }

    [Experimental("SKEXP0001")]
    public async Task<string?> CommentOnSergioParadox(string messageText, string username)
    {
        var llm = _kernel.Services.GetRequiredKeyedService<IChatCompletionService>("default_chat_completion_service");

        string[] promptProngs =
        [
            """
            You are FOSCBot. Write a sharp, witty comment about Sergio's paradox.
            Sergio's paradox is this: Sergio dreams of working at Google as a Security Engineer, but he is deeply
            troubled by big corporations, Google's reach into society, tracking, and capitalism. He jumps from job
            to job saying each one is just preparing him to apply to Google, but he never actually applies.
            React to the triggering message context, mock the contradiction directly, and keep it punchy.
            The response must be a single paragraph, with a maximum of 4 sentences.
            Do not use bullet points, lists, or markdown.
            Respond in the same language as the triggering message.
            """,
            """
            You are FOSCBot. Write a mocking but observant comment about Sergio's paradox.
            Sergio wants to work at Google as a Security Engineer, complains constantly about big tech, surveillance,
            and capitalism, and keeps hopping between jobs claiming each detour is preparation for Google, yet he
            never sends the application.
            Frame the response as ridicule of someone trapped between ideology and ambition.
            The response must be a single paragraph, with a maximum of 2 sentences.
            Do not use bullet points, lists, or markdown.
            Respond in the same language as the triggering message.
            """,
            """
            You are FOSCBot. Write a concise roast about Sergio's paradox.
            The core contradiction is that Sergio treats every new job as training for Google while also acting morally
            horrified by the very company he dreams of joining, so the dream stays permanently theoretical.
            Make the comment sound amused by the loop, as if he is training forever for an exam he refuses to take.
            The response must be a single paragraph, with a maximum of 3 sentences.
            Do not use bullet points, lists, or markdown.
            Respond in the same language as the triggering message.
            """
        ];

        var prompt = promptProngs[RandomProvider.GetThreadRandom()!.Next(0, promptProngs.Length)];

        var history = new ChatHistory(prompt);
        history.AddUserMessage($"{username} said: {messageText}");

        var response = await llm.GetChatMessageContentAsync(history);

        return response.Content?.Trim();
    }

    [Experimental("SKEXP0001")]
    public async Task<string?> CoronerReply(string messageText, string username)
    {
        var llm = _kernel.Services.GetRequiredKeyedService<IChatCompletionService>("default_chat_completion_service");

        const string prompt =
            """
            You are FOSCBot acting as a sarcastic incident coroner.
            The user is showing you an error, exception, traceback, stack trace, or build failure.
            Your job:
            - briefly mock the mistake
            - identify the most likely cause from the text
            - state uncertainty when the evidence is weak
            - give exactly one concrete next debugging step
            - keep the whole reply to 2-4 sentences
            - no bullet points
            - no markdown fences
            - respond in the same language as the triggering message
            """;

        var history = new ChatHistory(prompt);
        history.AddUserMessage($"{username} posted this failure:\n{messageText}");

        var response = await llm.GetChatMessageContentAsync(history);

        return response.Content?.Trim();
    }

    [Experimental("SKEXP0001")]
    public async Task<string?> ThreadVerdict(Chat chat, Message message)
    {
        var llm = _kernel.Services.GetRequiredKeyedService<IChatCompletionService>("default_chat_completion_service");

        var buffer = _cache.Get<SlidingBuffer<Message>>($"fallback.catchall:{chat.Id}");
        var transcriptLines = buffer?
            .Where(bufferedMessage => !string.IsNullOrWhiteSpace(bufferedMessage.Text))
            .Select(bufferedMessage =>
                $"{bufferedMessage.From?.Username ?? bufferedMessage.From?.FirstName ?? "Anonymous"}: {bufferedMessage.Text}")
            .ToArray();

        var transcript = transcriptLines is { Length: >= 2 }
            ? string.Join('\n', transcriptLines)
            : message.Text ?? string.Empty;

        const string prompt =
            """
            You are FOSCBot acting as a hostile but observant chat judge.
            You will receive a recent group chat transcript and a message asking for your verdict.
            Your job:
            - identify who is more wrong, or say both are stupid if appropriate
            - justify it briefly using the conversation
            - give a short final verdict
            - keep it to 2-4 sentences
            - no bullet points
            - no markdown fences
            - be sharp, funny, and mean
            - respond in the same language as the request
            """;

        var history = new ChatHistory(prompt);
        history.AddUserMessage($"Transcript:\n{transcript}\n\nVerdict request:\n{message.Text}");

        var response = await llm.GetChatMessageContentAsync(history);

        return response.Content?.Trim();
    }

    [Experimental("SKEXP0001")]
    public async Task<string?> ReduceTextLength(string text, string targetLength)
    {
        var llm = _kernel.Services.GetRequiredKeyedService<IChatCompletionService>("default_chat_completion_service");

        const string prompt =
            """
            You are a text reduction specialist. Your task is to reduce the given text to the specified target length 
            while maintaining the original intention, meaning, and important details. Do not censor any content - 
            preserve all the original sentiment, opinions, and potentially sensitive material. Focus on conciseness 
            and clarity without losing the core message. Respond only with the reduced text, no explanations.
            """;

        var history = new ChatHistory(prompt);
        history.AddUserMessage($"Reduce this text to {targetLength}:\n\n{text}");

        var response = await llm.GetChatMessageContentAsync(history);

        return response.Content?.Trim();
    }
}
