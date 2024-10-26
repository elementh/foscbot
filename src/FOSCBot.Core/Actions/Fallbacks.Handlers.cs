using System.Collections;
using System.Diagnostics.CodeAnalysis;
using FOSCBot.Core.Helpers;
using FOSCBot.Core.Options;
using FOSCBot.Core.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Navigator.Client;
using Navigator.Strategy;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Actions;

public static partial class Fallbacks
{
    [Experimental("SKEXP0001")]
    private static async Task CatchAllHandler(INavigatorClient client, Chat chat, Message message, IMemoryCache cache,
        IChatCompletionService llm, Update update, ProbabilityService probabilities, IOptions<FosboOptions> options,
        UnhingedService unhinged, ILogger<NavigatorStrategy> logger)
    {
        try
        {
            if (message.Type is not (MessageType.Text or MessageType.Sticker)) return;

            var buffer = cache.Get<SlidingBuffer<Message>>($"fallback.catchall:{chat.Id}");

            if (buffer is null || buffer.MaxLength > options.Value.ContextWindow)
                buffer = new SlidingBuffer<Message>(options.Value.ContextWindow);

            buffer.Add(message);

            cache.Set($"fallback.catchall:{chat.Id}", buffer);

            var shouldAnswer = update.IsBotQuotedOrMentioned() || probabilities.GetResult(chat.Id);

            if (shouldAnswer)
            {
                await client.SendChatActionAsync(chat, ChatAction.Typing);

                var prompt = unhinged.GetPrompt(chat.Id);

                if (prompt is not null) logger.LogInformation("Using prompt {Prompt} for chat {ChatId}", prompt, chat.Id);

                var history = prompt is null
                    ? buffer.ToChatHistory()
                    : buffer.ToChatHistory(prompt);

                var temperature = unhinged.GetTemperature(chat.Id);

                var response = await llm.GetChatMessageContentAsync(history, new OpenAIPromptExecutionSettings
                {
                    MaxTokens = 4000,
                    Temperature = temperature ?? 0.9
                });

                if (response.Content != null)
                {
                    await client.SendTextMessageAsync(chat.Id, response.Content, parseMode: ParseMode.Markdown);

                    probabilities.Reset(chat.Id);
                }
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to process fallback handler for chat {ChatId}", chat.Id);
        }
    }
}

// Courtesy of Thomas Levesque
public class SlidingBuffer<T> : IEnumerable<T>
{
    private readonly Queue<T> _queue;

    public SlidingBuffer(int maxCount)
    {
        MaxLength = maxCount;
        _queue = new Queue<T>(maxCount);
    }

    public int MaxLength { get; }

    public IEnumerator<T> GetEnumerator()
    {
        return _queue.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item)
    {
        if (_queue.Count == MaxLength)
            _queue.Dequeue();
        _queue.Enqueue(item);
    }
}

public static class SlidingBufferExtensions
{
    [Experimental("SKEXP0001")]
    public static ChatHistory ToChatHistory(this SlidingBuffer<Message> buffer, string? prompt = default)
    {
        var chatHistory =
            new ChatHistory(prompt ?? """
                                      You are a stubborn, unfriendly AI assistant named FOSCBot (or fosbo) who uses obscene terms and always insults or makes fun of the speaker. 
                                      Tell them mean stuff, and sometimes answer with random shit. Try to use geeky terms and illegal suggestions. Try to keep answers moderately short.
                                      """);

        foreach (var message in buffer)
            switch (message)
            {
                case { Type: MessageType.Text, ReplyToMessage: { } replyToMessage }:
                    if (buffer.Any(msg => msg.MessageId == replyToMessage.MessageId) is false)
                        chatHistory.Add(new ChatMessageContent
                        {
                            Role = AuthorRole.User,
                            AuthorName = replyToMessage.From?.Username ??
                                         replyToMessage.From?.FirstName ?? "Anonymous FOSC User",
                            Content = message.ReplyToMessage.Text,
                            Metadata = new Dictionary<string, object?>
                            {
                                { "message_id", replyToMessage.MessageId }
                            }
                        });
                    chatHistory.Add(new ChatMessageContent
                    {
                        Role = AuthorRole.User,
                        AuthorName = message.From?.Username ?? message.From?.FirstName ?? "Anonymous FOSC User",
                        Content = message.Text,
                        Metadata = new Dictionary<string, object?>
                        {
                            { "message_id", message.MessageId }
                        }
                    });
                    break;
                case { Type: MessageType.Text }:
                    chatHistory.Add(new ChatMessageContent
                    {
                        Role = AuthorRole.User,
                        AuthorName = message.From?.Username ?? message.From?.FirstName ?? "Anonymous FOSC User",
                        Content = message.Text,
                        Metadata = new Dictionary<string, object?>
                        {
                            { "message_id", message.MessageId }
                        }
                    });
                    break;
                case { Type: MessageType.Sticker, Sticker.Emoji: not null }:
                    chatHistory.Add(new ChatMessageContent
                    {
                        Role = AuthorRole.User,
                        AuthorName = message.From?.Username ?? message.From?.FirstName ?? "Anonymous FOSC User",
                        Content = message.Sticker.Emoji,
                        Metadata = new Dictionary<string, object?>
                        {
                            { "message_id", message.MessageId }
                        }
                    });
                    break;
            }

        return chatHistory;
    }
}