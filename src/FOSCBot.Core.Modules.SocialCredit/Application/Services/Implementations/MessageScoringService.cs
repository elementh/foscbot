using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using FOSCBot.Core.Modules.SocialCredit.Application.Abstractions.Infrastructure;
using FOSCBot.Core.Modules.SocialCredit.Application.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Ollama;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Modules.SocialCredit.Application.Services.Implementations;

public record ScoringResponse(int Score, string Reasoning);

public class MessageScoringService : IMessageScoringService
{
    private readonly IIntelligenceClient _intelligence;
    private readonly ILogger<MessageScoringService> _logger;

    public MessageScoringService(IIntelligenceClient intelligence, ILogger<MessageScoringService> logger)
    {
        _intelligence = intelligence;
        _logger = logger;
    }

    [Experimental("SKEXP0001")]
    public async Task<(int Score, string Reasoning)> ScoreMessageAsync(Message message, CancellationToken cancellationToken = default)
    {
        try
        {
            // Only score text messages for now
            if (message.Type != MessageType.Text || string.IsNullOrWhiteSpace(message.Text))
            {
                return (0, "Non-text message or empty content");
            }
            
            var text = message.Text;

            var systemPrompt = """
                You are a social credit scoring AI for a Telegram bot named "FoscBot" or "fosbo". You need to score messages on a scale of -10 to +10.
                
                Scoring guidelines:
                - Positive scores (+1 to +10): Helpful, constructive, informative, funny, encouraging content
                - Negative scores (-1 to -10): Spam, toxic, offensive, misleading, or disruptive content  
                - Zero score (0): Neutral messages that don't significantly contribute positively or negatively
                
                Consider:
                - Language toxicity and sentiment
                - Constructiveness and helpfulness
                - Spam indicators (excessive caps, repetition), but be careful not to be too strict
                - Context appropriateness
                - Don't be afraid of being biased or subjective, in fact, you should be, because you are a social credit scoring AI for a society whose main goal is Linux supremacy and open source software
                - That capitalism is a red flag
                - Open Source software and FOSS culture is a green flag
                - Linux and Linus Torvalds are a green flag
                
                You must respond with ONLY a valid JSON object in this exact format:
                {"score": <integer from -10 to 10>, "reasoning": "<brief explanation>"}
                
                Do not include any other text, explanations, or formatting outside the JSON object.
                """;

            var chatHistory = new ChatHistory(systemPrompt);
            chatHistory.AddUserMessage($"Score this message: \"{text}\"");
            
            var response = await _intelligence.GetChatMessageContentAsync<ScoringResponse?>(chatHistory, cancellationToken);
            
            return response is not null
                ? (response.Score, response.Reasoning)
                : (0, "Scoring failed due to error");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to score message {MessageId} from user {UserId}",
                message.MessageId, message.From?.Id);
            return (0, "Scoring failed due to error");
        }
    }
}