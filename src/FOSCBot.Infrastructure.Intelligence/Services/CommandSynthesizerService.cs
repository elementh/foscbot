using System.Diagnostics.CodeAnalysis;
using FOSCBot.Core.Application.Abstractions;
using FOSCBot.Core.Module.Models;
using FOSCBot.Core.Module.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace FOSCBot.Infrastructure.Intelligence.Services;

internal class CommandSynthesizerService : ICommandSynthesizerService
{
    private readonly Kernel _kernel;
    private readonly PhantomCommandOptions _options;

    public CommandSynthesizerService(Kernel kernel, IOptions<PhantomCommandOptions> options)
    {
        _kernel = kernel;
        _options = options.Value;
    }

    [Experimental("SKEXP0001")]
    public async Task<(string Description, string Personality)?> GenerateCommandDescription(string commandName, string? arguments)
    {
        var llm = _kernel.Services.GetRequiredKeyedService<IChatCompletionService>(_options.ServiceId);

        var personality = _options.Personalities[Random.Shared.Next(_options.Personalities.Length)];
        var prompt = _options.GenerationTemplate.Replace("{Personality}", personality);

        var history = new ChatHistory(prompt);
        history.AddUserMessage($"Command: /{commandName} Arguments: {arguments ?? "none"}");

        var response = await llm.GetChatMessageContentAsync(history);
        if (response.Content is null)
            return null;

        var raw = response.Content.Trim();
        if (raw.StartsWith("```"))
            raw = StripCodeFences(raw);

        var definition = PhantomCommandDefinition.TryParse(raw);
        if (definition is null)
            return null;

        var normalized = definition.Serialize();
        return (normalized, personality);
    }

    [Experimental("SKEXP0001")]
    public async Task<string?> ExecutePhantomCommand(string description, string? arguments, string username, string personality, string? replyContext = null)
    {
        var llm = _kernel.Services.GetRequiredKeyedService<IChatCompletionService>(_options.ServiceId);

        var definition = PhantomCommandDefinition.TryParse(description);
        if (definition is null)
            return null;

        var prompt = _options.ExecutionTemplate
            .Replace("{Personality}", personality)
            .Replace("{Description}", definition.Description)
            .Replace("{OutputFormat}", definition.OutputFormat)
            .Replace("{ResponseLength}", definition.ResponseLength)
            .Replace("{Tone}", definition.Tone)
            .Replace("{Constraints}", definition.FormatConstraints())
            .Replace("{Examples}", definition.FormatExamples());

        var history = new ChatHistory(prompt);

        var userMessage = $"{username} used the command with: {arguments ?? "no arguments"}";
        if (replyContext is not null)
            userMessage += $"\nReplying to message: {replyContext}";

        history.AddUserMessage(userMessage);

        var response = await llm.GetChatMessageContentAsync(history);

        return response.Content;
    }

    private static string StripCodeFences(string text)
    {
        var lines = text.Split('\n');
        var start = 0;
        var end = lines.Length - 1;

        if (lines[start].StartsWith("```"))
            start++;
        if (end > start && lines[end].TrimEnd() == "```")
            end--;

        return string.Join('\n', lines[start..(end + 1)]).Trim();
    }
}
