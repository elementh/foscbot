using FOSCBot.Core.Application.Abstractions;
using FOSCBot.Infrastructure.Intelligence.Abstractions.Client;
using Microsoft.SemanticKernel.ChatCompletion;

namespace FOSCBot.Infrastructure.Intelligence.Services;

internal class TextMeaningService : ITextMeaningService
{
    private const string Prompt =
        """
        You classify whether a message matches a requested meaning.
        Return JSON only in this shape:
        { "isMatch": true }

        Rules:
        - Be strict.
        - If the message only mentions related names or topics without actually asking for that meaning, return false.
        - Use sender context only to resolve self-reference like "me", "I", "yo", or "mí".
        - Ignore wording differences, jokes, and minor mistakes if the intent is still clearly the requested meaning.
        """;

    private readonly IIntelligenceClient _intelligenceClient;

    public TextMeaningService(IIntelligenceClient intelligenceClient)
    {
        _intelligenceClient = intelligenceClient;
    }

    public async Task<bool> MatchesMeaning(string text, string meaning, TextMeaningContext context, CancellationToken cancellationToken = default)
    {
        var history = new ChatHistory(Prompt);
        history.AddUserMessage(
            $"""
             Meaning to detect:
             {meaning}

             Sender context:
             - username: {context.SenderUsername ?? "unknown"}
             - display_name: {context.SenderDisplayName ?? "unknown"}

             Message:
             {text}
             """);

        var response = await _intelligenceClient.GetChatMessageContentAsync<TextMeaningMatch>(history, cancellationToken);

        return response?.IsMatch is true;
    }

    private sealed class TextMeaningMatch
    {
        public bool IsMatch { get; set; }
    }
}
