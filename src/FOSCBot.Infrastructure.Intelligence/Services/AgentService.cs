using System.Diagnostics.CodeAnalysis;
using FOSCBot.Core.Application.Abstractions;
using FOSCBot.Core.Common;
using FOSCBot.Core.Module.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Ollama;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Infrastructure.Intelligence.Services;

internal class AgentService : IAgentService
{
    private readonly IMemoryCache _cache;
    private readonly Kernel _kernel;
    private readonly UnhingedService _unhingedService;
    private readonly FosboOptions _options;

    public AgentService(IMemoryCache cache, Kernel kernel, UnhingedService unhingedService, IOptions<FosboOptions> options)
    {
        _cache = cache;
        _kernel = kernel;
        _unhingedService = unhingedService;
        _options = options.Value;
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

        buffer.Add(message);

        var prompt = _unhingedService.GetPrompt(chat.Id) ?? _options.DefaultPrompt;
        var history = buffer.ToChatHistory(prompt);

        var executionSettings = new OllamaPromptExecutionSettings { };

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

        const string prompt =
            """
            You are FOSCBot. Write a sharp, witty comment about Sergio's paradox.
            Sergio's paradox is this: Sergio dreams of working at Google as a Security Engineer, but he is deeply
            troubled by big corporations, Google's reach into society, tracking, and capitalism. He jumps from job
            to job saying each one is just preparing him to apply to Google, but he never actually applies.
            React to the triggering message context, mock the contradiction, and keep it punchy.
            The response must be a single paragraph, with a maximum of 4 sentences.
            Do not use bullet points, lists, or markdown.
            Respond in the same language as the triggering message.
            """;

        var history = new ChatHistory(prompt);
        history.AddUserMessage($"{username} said: {messageText}");

        var response = await llm.GetChatMessageContentAsync(history);

        return response.Content?.Trim();
    }
}
