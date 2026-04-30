namespace FOSCBot.Core.Application.Abstractions;

public interface ITextMeaningService
{
    Task<bool> MatchesMeaning(string text, string meaning, TextMeaningContext context, CancellationToken cancellationToken = default);
}

public readonly record struct TextMeaningContext(string? SenderUsername, string? SenderDisplayName);
