using System;
using System.Diagnostics.CodeAnalysis;
using FOSCBot.Core.Helpers;
using FOSCBot.Core.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Services;

public class AgentService
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
        var llm = _kernel.Services.GetRequiredService<IChatCompletionService>();
        
        if (message.Type is not (MessageType.Text or MessageType.Sticker))
            return null;

        var buffer = _cache.Get<SlidingBuffer<Message>>($"fallback.catchall:{chat.Id}");

        if (buffer is null || buffer.MaxLength > _options.ContextWindow)
            buffer = new SlidingBuffer<Message>(_options.ContextWindow);

        buffer.Add(message);
        _cache.Set($"fallback.catchall:{chat.Id}", buffer);

        var prompt = _unhingedService.GetPrompt(chat.Id);
        var history = prompt is null
            ? buffer.ToChatHistory()
            : buffer.ToChatHistory(prompt);

        var temperature = _unhingedService.GetTemperature(chat.Id);

        var response = await llm.GetChatMessageContentAsync(history, new OpenAIPromptExecutionSettings
        {
            MaxTokens = 5_000,
            Temperature = temperature ?? 1
        });

        return response.Content;
    }
}
