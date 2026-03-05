using System.Diagnostics.CodeAnalysis;
using FOSCBot.Core.Application.Abstractions;
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
        var llm = _kernel.Services.GetRequiredKeyedService<IChatCompletionService>("default_chat_completion_service");

        var personality = _options.Personalities[Random.Shared.Next(_options.Personalities.Length)];
        var prompt = _options.GenerationTemplate.Replace("{Personality}", personality);

        var history = new ChatHistory(prompt);
        history.AddUserMessage($"Command: /{commandName} Arguments: {arguments ?? "none"}");

        var response = await llm.GetChatMessageContentAsync(history);

        return response.Content is not null ? (response.Content, personality) : null;
    }

    [Experimental("SKEXP0001")]
    public async Task<string?> ExecutePhantomCommand(string description, string? arguments, string username, string personality, string? replyContext = null)
    {
        var llm = _kernel.Services.GetRequiredKeyedService<IChatCompletionService>("default_chat_completion_service");

        var prompt = _options.ExecutionTemplate
            .Replace("{Personality}", personality)
            .Replace("{Description}", description);

        var history = new ChatHistory(prompt);

        var userMessage = $"{username} used the command with: {arguments ?? "no arguments"}";
        if (replyContext is not null)
            userMessage += $"\nReplying to message: {replyContext}";

        history.AddUserMessage(userMessage);

        var response = await llm.GetChatMessageContentAsync(history);

        return response.Content;
    }
}
