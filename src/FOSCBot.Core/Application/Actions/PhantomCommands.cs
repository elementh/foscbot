using System.Diagnostics.CodeAnalysis;
using System.Text;
using FOSCBot.Core.Application.Abstractions;
using Incremental.Common.Random;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Client;
using Navigator.Abstractions.Priorities;
using Navigator.Actions.Builder.Extensions;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
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
        catalog.OnCommandPattern("^(?!felicidades).*", PhantomCommandHandler)
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

            var spaceIndex = text.IndexOf(' ');
            var commandName = (spaceIndex > 0 ? text[1..spaceIndex] : text[1..]).ToLowerInvariant();
            var arguments = spaceIndex > 0 ? text[(spaceIndex + 1)..].Trim() : null;

            if (string.IsNullOrWhiteSpace(commandName)) return;

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
                await client.SendMessageDraft(chat.Id, streamId, responseBuilder.ToString(), parseMode: ParseMode.Markdown);
            }
            
            await client.SendMessage(chat, responseBuilder.ToString(), parseMode: ParseMode.Markdown, replyParameters: message);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to process phantom command in chat {ChatId}", chat.Id);
        }
    }
}
