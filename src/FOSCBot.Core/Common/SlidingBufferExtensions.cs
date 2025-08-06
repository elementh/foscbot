using System.Diagnostics.CodeAnalysis;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Common;

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