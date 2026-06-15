using System.Diagnostics.CodeAnalysis;

namespace FOSCBot.Core.Application.Abstractions;

public interface ICommandSynthesizerService
{
    [Experimental("SKEXP0001")]
    Task<(string Description, string Personality)?> GenerateCommandDescription(string commandName, string? arguments);

    [Experimental("SKEXP0001")]
    Task<string?> ExecutePhantomCommand(string description, string? arguments, string username, string personality, string? replyContext = null);

    [Experimental("SKEXP0001")]
    IAsyncEnumerable<string> ExecutePhantomCommandStream(string description, string? arguments, string username,
        string personality, string? replyContext = null, CancellationToken cancellationToken = default);
}
