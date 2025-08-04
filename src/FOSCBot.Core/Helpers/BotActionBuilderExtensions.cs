using Incremental.Common.Random;
using Navigator.Abstractions.Client;
using Navigator.Actions.Builder;
using Navigator.Actions.Builder.Extensions;
using Navigator.Client;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Chat = Telegram.Bot.Types.Chat;

namespace FOSCBot.Core.Helpers;

public static class BotActionBuilderExtensions
{
    public static BotActionBuilder SendPhoto(this BotActionBuilder builder, string photo, string? caption = null, bool asReply = false,
        bool toReply = false)
    {
        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var replyParameters = default(ReplyParameters);

            if (asReply && message is not null) replyParameters = message;

            if (toReply && message?.ReplyToMessage is not null) replyParameters = message.ReplyToMessage;

            await client.SendPhoto(chat, photo, caption: caption, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.UploadPhoto);

        return builder;
    }

    public static BotActionBuilder SendRandomPhotoFrom(this BotActionBuilder builder, string[] photos, bool asReply = false,
        bool toReply = false)
    {
        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (photos.Length == 0 || chat is null) return;

            var randomPhoto = photos.Length switch
            {
                1 => photos[0],
                _ => photos[RandomProvider.GetThreadRandom()!.Next(0, photos.Length)]
            };

            var replyParameters = default(ReplyParameters);

            if (asReply && message is not null) replyParameters = message;

            if (toReply && message?.ReplyToMessage is not null) replyParameters = message.ReplyToMessage;

            await client.SendPhoto(chat, randomPhoto, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.UploadPhoto);

        return builder;
    }


    public static BotActionBuilder SendSticker(this BotActionBuilder builder, string sticker, bool asReply = false,
        bool toReply = false)
    {
        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var replyParameters = default(ReplyParameters);

            if (asReply && message is not null) replyParameters = message;

            if (toReply && message?.ReplyToMessage is not null) replyParameters = message.ReplyToMessage;

            await client.SendSticker(chat, sticker, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.ChooseSticker);

        return builder;
    }

    /// <summary>
    ///     Send a random sticker from the provided list.
    /// </summary>
    /// <param name="builder">The <see cref="BotActionBuilder" /></param>
    /// <param name="stickers">The list of stickers</param>
    /// <param name="asReply">If the sticker should be sent as a reply</param>
    /// <param name="toReply">If the sticker should be sent as a reply to the message the original message was replying to.</param>
    /// <returns>The <see cref="BotActionBuilder" /></returns>
    public static BotActionBuilder SendRandomStickerFrom(this BotActionBuilder builder, string[] stickers, bool asReply = false,
        bool toReply = false)
    {
        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (stickers.Length == 0 || chat is null) return;

            var randomSticker = stickers.Length switch
            {
                1 => stickers[0],
                _ => stickers[RandomProvider.GetThreadRandom()!.Next(0, stickers.Length)]
            };

            var replyParameters = default(ReplyParameters);

            if (asReply && message is not null) replyParameters = message;

            if (toReply && message?.ReplyToMessage is not null) replyParameters = message.ReplyToMessage;

            await client.SendSticker(chat, randomSticker, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.ChooseSticker);

        return builder;
    }

    public static BotActionBuilder SendText(this BotActionBuilder builder, string text, ParseMode parseMode = ParseMode.Markdown,
        bool asReply = false, bool toReply = false)
    {
        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var replyParameters = default(ReplyParameters);

            if (asReply && message is not null) replyParameters = message;

            if (toReply && message?.ReplyToMessage is not null) replyParameters = message.ReplyToMessage;

            await client.SendMessage(chat, text, parseMode: parseMode, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.Typing);

        return builder;
    }

    public static BotActionBuilder SendRandomTextFrom(this BotActionBuilder builder, string[] texts,
        ParseMode parseMode = ParseMode.Markdown,
        bool asReply = false, bool toReply = false)
    {
        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (texts.Length == 0 || chat is null) return;

            var randomText = texts.Length switch
            {
                1 => texts[0],
                _ => texts[RandomProvider.GetThreadRandom()!.Next(0, texts.Length)]
            };

            var replyParameters = default(ReplyParameters);

            if (asReply && message is not null) replyParameters = message;

            if (toReply && message?.ReplyToMessage is not null) replyParameters = message.ReplyToMessage;

            await client.SendMessage(chat, randomText, parseMode: parseMode, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.Typing);

        return builder;
    }

    public static BotActionBuilder SendVideo(this BotActionBuilder builder, string video, string? caption = null, bool asReply = false,
        bool toReply = false)

    {
        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (chat is null) return;

            var replyParameters = default(ReplyParameters);

            if (asReply && message is not null) replyParameters = message;

            if (toReply && message?.ReplyToMessage is not null) replyParameters = message.ReplyToMessage;

            await client.SendVideo(chat, video, caption: caption, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.UploadVideo);

        return builder;
    }

    public static BotActionBuilder SendRandomVideoFrom(this BotActionBuilder builder, string[] videos, bool asReply = false,
        bool toReply = false)
    {
        builder.SetHandler(async (INavigatorClient client, Chat? chat, Message? message) =>
        {
            if (videos.Length == 0 || chat is null) return;

            var randomVideo = videos.Length switch
            {
                1 => videos[0],
                _ => videos[RandomProvider.GetThreadRandom()!.Next(0, videos.Length)]
            };

            var replyParameters = default(ReplyParameters);

            if (asReply && message is not null) replyParameters = message;

            if (toReply && message?.ReplyToMessage is not null) replyParameters = message.ReplyToMessage;

            await client.SendVideo(chat, randomVideo, replyParameters: replyParameters);
        });

        builder.WithChatAction(ChatAction.UploadVideo);

        return builder;
    }
}