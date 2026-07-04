using System.Diagnostics.CodeAnalysis;
using System.Text;
using FOSCBot.Core.Application.Abstractions;
using Incremental.Common.Random;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Actions.Builder.Extensions;
using Navigator.Abstractions.Catalog.Extensions;
using Navigator.Abstractions.Client;
using Navigator.Catalog.Factory;
using Navigator.Extensions.Cooldown.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Application.Actions;

public static class PhantomCommands
{
    [Experimental("SKEXP0001")]
    public static void RegisterPhantomCommands(this BotActionCatalogFactory catalog)
    {
        catalog.OnCommandPattern("^(?i)(?!felicidades).*", PhantomCommandHandler)
            .WithChatAction(ChatAction.Typing)
            .WithCooldown(TimeSpan.FromSeconds(5))
            .WithName("PhantomCommands");
    }

    [Experimental("SKEXP0001")]
    private static async Task PhantomCommandHandler(
        INavigatorClient client,
        Chat chat,
        Message message,
        IPhantomCommandService phantomCommandService,
        ICommandSynthesizerService commandSynthesizer,
        ILogger<ICommandSynthesizerService> logger)
    {
        try
        {
            var text = message.Text;
            if (string.IsNullOrWhiteSpace(text)) return;

            // Telegram marks bot_command entities anywhere in the message ("hola /etc roto"
            // used to invent the command "ola"); only honor a command that starts the
            // message and is addressed to nobody or to this bot.
            var commandEntity = message.Entities?.FirstOrDefault();
            if (commandEntity is null || commandEntity.Type != MessageEntityType.BotCommand || commandEntity.Offset != 0) return;

            var rawCommand = text.Substring(1, commandEntity.Length - 1);
            var atIndex = rawCommand.IndexOf('@');

            if (atIndex >= 0)
            {
                if (!rawCommand[(atIndex + 1)..].Equals("foscbot", StringComparison.OrdinalIgnoreCase)) return;

                rawCommand = rawCommand[..atIndex];
            }

            var commandName = rawCommand.ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(commandName)) return;

            var arguments = text.Length > commandEntity.Length ? text[commandEntity.Length..].Trim() : null;
            if (string.IsNullOrWhiteSpace(arguments)) arguments = null;

            var existing = await phantomCommandService.GetCommandAsync(commandName, chat.Id);
            string description;
            string personality;

            if (existing is not null)
            {
                description = existing.Description;
                personality = existing.Personality;
            }
            else
            {
                var result = await commandSynthesizer.GenerateCommandDescription(commandName, arguments);

                if (result is null) return;

                (description, personality) = result.Value;
                await phantomCommandService.SaveCommandAsync(commandName, description, personality, chat.Id);
            }

            var replyAuthor = message.ReplyToMessage?.From?.Username ?? message.ReplyToMessage?.From?.FirstName;
            var replyContext = message.ReplyToMessage?.Text is not null
                ? $"{replyAuthor ?? "Someone"} said: {message.ReplyToMessage.Text}"
                : null;

            var responseBuilder = new StringBuilder();

            var streamId = RandomProvider.GetThreadRandom()!.Next();
            await foreach (var chunk in commandSynthesizer.ExecutePhantomCommandStream(description, arguments,
                               message.From?.Username ?? message.From?.FirstName ?? "Anonymous", personality,
                               replyContext))
            {
                responseBuilder.Append(chunk);

                if (chat.Type == ChatType.Private)
                {
                    await client.SendMessageDraft(chat.Id, streamId, responseBuilder.ToString(), parseMode: ParseMode.Markdown);
                }
            }
            
            await client.SendMessage(chat, responseBuilder.ToString(), parseMode: ParseMode.Markdown, replyParameters: message);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to process phantom command in chat {ChatId}", chat.Id);
        }
    }
}
