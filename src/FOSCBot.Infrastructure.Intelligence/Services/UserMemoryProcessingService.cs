#pragma warning disable SKEXP0001
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using FOSCBot.Common.Persistence.Entities;
using FOSCBot.Core.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace FOSCBot.Infrastructure.Intelligence.Services;

internal sealed class UserMemoryProcessingService : BackgroundService
{
    private const string ProfilingServiceId = "profiling_service";
    private const int MaxContentLength = 1024;

    private readonly UserMemoryChannel _channel;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<UserMemoryProcessingService> _logger;

    public UserMemoryProcessingService(UserMemoryChannel channel, IServiceProvider serviceProvider,
        ILogger<UserMemoryProcessingService> logger)
    {
        _channel = channel;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var batch in _channel.Channel.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                await ProcessBatchAsync(scope.ServiceProvider, batch, stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to process memory batch for chat {ChatId}", batch.ChatId);
            }
        }
    }

    [Experimental("SKEXP0001")]
    private async Task ProcessBatchAsync(IServiceProvider scope, MemoryBatch batch, CancellationToken ct)
    {
        var kernel = scope.GetRequiredService<Kernel>();
        var llm = kernel.Services.GetKeyedService<IChatCompletionService>(ProfilingServiceId);

        if (llm is null)
        {
            _logger.LogWarning("Profiling service '{ServiceId}' not found, skipping memory extraction", ProfilingServiceId);
            return;
        }

        var userMemoryService = scope.GetRequiredService<IUserMemoryService>();

        var transcript = string.Join('\n',
            batch.Messages.Select(m => $"{m.Username} (id:{m.UserId}): {m.Text}"));

        var extractionPrompt = """
            You are a profiling assistant. Given a batch of chat messages from multiple users, extract one notable one-liner fact per user (preferences, trivia, personality traits, opinions).
            Return JSON: [{"userId": 123, "username": "foo", "suggestion": "one-liner fact"}]
            If nothing notable for a user, omit them. Respond ONLY with JSON.
            """;

        var history = new ChatHistory(extractionPrompt);
        history.AddUserMessage(transcript);

        string? extractionResponse;
        try
        {
            var response = await llm.GetChatMessageContentAsync(history, cancellationToken: ct);
            extractionResponse = response.Content?.Trim();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Extraction LLM call failed for chat {ChatId}", batch.ChatId);
            return;
        }

        if (string.IsNullOrWhiteSpace(extractionResponse))
        {
            _logger.LogDebug("Empty extraction response for chat {ChatId}", batch.ChatId);
            return;
        }

        if (extractionResponse.StartsWith("```"))
            extractionResponse = StripCodeFences(extractionResponse);

        List<ExtractionResult>? suggestions;
        try
        {
            suggestions = JsonSerializer.Deserialize<List<ExtractionResult>>(extractionResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to parse extraction JSON for chat {ChatId}. Raw: {Raw}", batch.ChatId, extractionResponse);
            return;
        }

        if (suggestions is null or { Count: 0 })
        {
            _logger.LogDebug("No suggestions extracted for chat {ChatId}", batch.ChatId);
            return;
        }

        foreach (var suggestion in suggestions)
        {
            try
            {
                await MergeAndSaveAsync(llm, userMemoryService, suggestion, ct);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to merge memory for user {UserId} in chat {ChatId}", suggestion.UserId, batch.ChatId);
            }
        }
    }

    [Experimental("SKEXP0001")]
    private async Task MergeAndSaveAsync(IChatCompletionService llm, IUserMemoryService userMemoryService,
        ExtractionResult suggestion, CancellationToken ct)
    {
        var existing = await userMemoryService.GetAsync(suggestion.UserId);
        var existingContent = existing?.Content ?? "<NO PROFILE BUILT YET>";

        var mergePrompt = """
            You are a memory merge assistant. You have an existing user profile and a new observation.
            Merge the new observation into the profile. If the observation redundant, respond with exactly "DISCARD".
            Otherwise respond with the merged profile text. Keep it concise and factual. Do not use bullet points or markdown.
            """;

        var history = new ChatHistory(mergePrompt);
        history.AddUserMessage($"Existing: {existingContent}\nNew: {suggestion.Suggestion}");

        string? merged;
        try
        {
            var response = await llm.GetChatMessageContentAsync(history, cancellationToken: ct);
            merged = response.Content?.Trim();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Merge LLM call failed for user {UserId}", suggestion.UserId);
            return;
        }

        if (string.IsNullOrWhiteSpace(merged) || merged.Equals("DISCARD", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogDebug("Discarded redundant memory suggestion for user {UserId} ({Username})", suggestion.UserId,
                suggestion.Username);
            return;
        }

        if (merged.Length > MaxContentLength)
        {
            merged = await ReduceAsync(llm, merged, ct) ?? existingContent;
        }

        var memory = new UserMemory
        {
            Id = existing?.Id ?? Guid.NewGuid(),
            TelegramUserExternalId = suggestion.UserId,
            Content = merged,
            CreatedAt = existing?.CreatedAt ?? DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        await userMemoryService.SaveAsync(memory);

        _logger.LogInformation("Memory updated for user {UserId} ({Username})", suggestion.UserId, suggestion.Username);
    }

    [Experimental("SKEXP0001")]
    private async Task<string?> ReduceAsync(IChatCompletionService llm, string text, CancellationToken ct)
    {
        var prompt = """
            You are a text reduction specialist. Your task is to reduce the given text to the specified target length
            while maintaining the original intention, meaning, and important details. Do not censor any content -
            preserve all the original sentiment, opinions, and potentially sensitive material. Focus on conciseness
            and clarity without losing the core message. Respond only with the reduced text, no explanations.
            """;

        var history = new ChatHistory(prompt);
        history.AddUserMessage($"Reduce this text to 1024 characters:\n\n{text}");

        try
        {
            var response = await llm.GetChatMessageContentAsync(history, cancellationToken: ct);
            return response.Content?.Trim();
        }
        catch
        {
            return null;
        }
    }

    private static string StripCodeFences(string raw)
    {
        var lines = raw.Split('\n');
        var start = 0;
        var end = lines.Length;

        if (lines[0].TrimStart().StartsWith("```"))
            start = 1;
        if (lines[^1].TrimStart().StartsWith("```"))
            end = lines.Length - 1;

        return string.Join('\n', lines[start..end]).Trim();
    }

    private sealed class ExtractionResult
    {
        public long UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Suggestion { get; set; } = string.Empty;
    }
}
