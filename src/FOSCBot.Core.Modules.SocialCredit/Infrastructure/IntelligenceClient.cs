using System.Text.Json;
using FOSCBot.Core.Modules.SocialCredit.Application.Abstractions.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Ollama;

namespace FOSCBot.Core.Modules.SocialCredit.Infrastructure;

public class IntelligenceClient : IIntelligenceClient
{
    private readonly Kernel _kernel;
    private readonly IntelligenceClientOptions _options;
    private readonly ILogger<IntelligenceClient> _logger;

    public IntelligenceClient(Kernel kernel, IOptions<IntelligenceClientOptions> options,
        ILogger<IntelligenceClient> logger)
    {
        _kernel = kernel;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<T?> GetChatMessageContentAsync<T>(ChatHistory history,
        CancellationToken cancellationToken = default)
    {
        var content = await GetChatCompletionAsync<T>(_options.DefaultServiceId, history, cancellationToken);

        if (content is null)
        {
            content = await GetChatCompletionAsync<T>(_options.BackupServiceId, history, cancellationToken);
        }

        return content;
    }

    private async Task<T?> GetChatCompletionAsync<T>(string serviceId, ChatHistory history, CancellationToken cancellationToken = default)
    {
        try
        {
            var chatCompletion = _kernel.Services.GetRequiredKeyedService<IChatCompletionService>(serviceId);

            var response = await chatCompletion.GetChatMessageContentAsync(history, _options.PromptExecutionSettings,
                cancellationToken: cancellationToken);

            if (response.Content is { Length: > 0 } content)
            {
                return JsonSerializer.Deserialize<T>(content, _options.SerializerOptions);
            }

            return default;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get chat completion for service {ServiceId}", serviceId);
            return default;
        }
    }
}

public class IntelligenceClientOptions
{
    public IntelligenceClientOptions()
    {
        SerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        PromptExecutionSettings = new OllamaPromptExecutionSettings
        {
            ExtensionData = new Dictionary<string, object>()
            {
                { "format", "json" }
            },
            Temperature = 0.8f,
        };
    }

    public JsonSerializerOptions SerializerOptions { get; set; }
    public OllamaPromptExecutionSettings PromptExecutionSettings { get; set; }

    public string DefaultServiceId { get; set; } = "default_chat_completion_service";
    public string BackupServiceId { get; set; } = "backup_chat_completion_service";
}