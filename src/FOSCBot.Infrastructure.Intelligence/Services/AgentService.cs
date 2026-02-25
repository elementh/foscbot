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

        var temperature = _unhingedService.GetTemperature(chat.Id);

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
}

