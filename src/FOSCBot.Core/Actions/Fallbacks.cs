using FOSCBot.Core.Helpers;
using FOSCBot.Infrastructure.Contract.Service;
using Incremental.Common.Random;
using Navigator.Actions;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Navigator.Client;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Actions;

public static class Fallbacks
{
    public static void RegisterFallbacks(this BotActionCatalogFactory catalog)
    {
        // Random Word Fallback
        catalog.OnText((string text) =>
        {
            if (RandomProvider.GetThreadRandom()!.NextDouble() > 0.6) return false;

            var word = text.Trim().Split(" ").FirstOrDefault() ?? string.Empty;
            return !string.IsNullOrWhiteSpace(word) && word.IsAllUpper() && word.Length >= 4 && !word.Contains("XDDD");
        }, async (INavigatorClient client, Chat chat, string text, IGiphyService giphy) =>
        {
            var gifUrl = await giphy.Get(text.Trim().Split(" ").FirstOrDefault() ?? string.Empty);
            
            if (gifUrl is not null)
            {
                await client.SendAnimationAsync(chat, new InputFileUrl(gifUrl));
            }
        }).WithCooldown(TimeSpan.FromMinutes(15)).WithPriority(Priority.Low - 100);
    }
}