using FOSCBot.Core.Module.Options;
using Incremental.Common.Random;
using Microsoft.Extensions.Options;
using Navigator.Abstractions.Actions.Builder.Extensions;
using Navigator.Abstractions.Catalog.Extensions;
using Navigator.Abstractions.Client;
using Navigator.Abstractions.Priorities;
using Navigator.Catalog.Factory;
using Navigator.Extensions.Cooldown.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Application.Actions;

public static class UserTriggered
{
    public static void RegisterUserTriggered(this BotActionCatalogFactory catalog)
    {
        catalog
            .OnText((Message message, IOptions<UserTriggeredResponseOptions> options) =>
                FindResponse(message, options.Value) is not null)
            .SetHandler(async (INavigatorClient client, Chat chat, Message message,
                IOptions<UserTriggeredResponseOptions> options) =>
            {
                var response = FindResponse(message, options.Value);

                if (response is null) return;

                await client.SendMessage(
                    chat,
                    response.Phrases[RandomProvider.GetThreadRandom()!.Next(0, response.Phrases.Length)],
                    parseMode: ParseMode.Markdown,
                    replyParameters: message);
            })
            .WithPriority(EPriority.AboveNormal)
            .WithCooldown(TimeSpan.FromMinutes(10))
            .WithName("UserTriggered.Responses");
    }

    private static UserTriggeredResponse? FindResponse(Message message, UserTriggeredResponseOptions options)
    {
        if (message.From is null || message.Text is null) return null;

        // A response with no phrases must not match: it would win the single-action
        // slot for this update and then reply with nothing.
        return options.Responses.FirstOrDefault(response => response.Phrases.Length > 0
            && MatchesUser(message.From, response)
            && MatchesText(message.Text, response));
    }

    private static bool MatchesUser(User from, UserTriggeredResponse response)
    {
        // The id is authoritative when configured: it survives username changes and
        // cannot be squatted. Username alone is the fallback for id-less entries.
        if (response.UserId is not null) return from.Id == response.UserId;

        return !string.IsNullOrWhiteSpace(response.Username) &&
               string.Equals(from.Username, response.Username.TrimStart('@'), StringComparison.InvariantCultureIgnoreCase);
    }

    private static bool MatchesText(string text, UserTriggeredResponse response)
    {
        if (text.Length < response.MinTextLength) return false;

        return response.TextContains.Length == 0 ||
               response.TextContains.Any(fragment => text.Contains(fragment, StringComparison.InvariantCultureIgnoreCase));
    }
}
