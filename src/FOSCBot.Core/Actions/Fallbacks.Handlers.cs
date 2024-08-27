using System.Collections;
using System.Diagnostics.CodeAnalysis;
using FOSCBot.Core.Helpers;
using FOSCBot.Core.Services;
using Incremental.Common.Random;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Navigator.Client;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Actions;

public static partial class Fallbacks
{
    [Experimental("SKEXP0001")]
    private static async Task CatchAllHandler(INavigatorClient client, Chat chat, Message message, IMemoryCache cache,
        IChatCompletionService llm, Update update, ProbabilityService probabilities)
    {
        try
        {
            if (message.Type is not (MessageType.Text or MessageType.Sticker)) return;

            var buffer = cache.Get<SlidingBuffer<Message>>($"fallback.catchall:{chat.Id}") ?? new SlidingBuffer<Message>(10);

            buffer.Add(message);

            cache.Set($"fallback.catchall:{chat.Id}", buffer);

            var shouldAnswer = update.IsBotQuotedOrMentioned()
                ? RandomProvider.GetThreadRandom()!.NextDouble() < 0.167
                : probabilities.GetResult($"fallback.catchall.probabilities:{chat.Id}");

            if (shouldAnswer)
            {
                await client.SendChatActionAsync(chat, ChatAction.Typing);

                var history = buffer.ToChatHistory();

                var response = await llm.GetChatMessageContentAsync(history, new OpenAIPromptExecutionSettings
                {
                    MaxTokens = 4000,
                    Temperature = 0.9
                });

                if (response.Content != null)
                {
                    await client.SendTextMessageAsync(chat.Id, response.Content);

                    probabilities.Reset($"fallback.catchall.probabilities:{chat.Id}");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}

// Courtesy of Thomas Levesque
public class SlidingBuffer<T> : IEnumerable<T>
{
    private readonly int _maxCount;
    private readonly Queue<T> _queue;

    public SlidingBuffer(int maxCount)
    {
        _maxCount = maxCount;
        _queue = new Queue<T>(maxCount);
    }

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
        if (_queue.Count == _maxCount)
            _queue.Dequeue();
        _queue.Enqueue(item);
    }
}

public static class SlidingBufferExtensions
{
    [Experimental("SKEXP0001")]
    public static ChatHistory ToChatHistory(this SlidingBuffer<Message> buffer)
    {
        var chatHistory =
            new ChatHistory("""
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