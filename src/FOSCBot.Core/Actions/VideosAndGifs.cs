using FOSCBot.Core.Resources;
using Incremental.Common.Random;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Navigator.Client;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Actions;

public static class VideosAndGifs
{
    public static void RegisterVideosAndGifs(this BotActionCatalogFactory catalog)
    {
        // Based
        catalog.OnText((string text) =>
        {
            return text.ToLower().Equals("based") || text.Equals("BASED");
        }, async (INavigatorClient client, Chat chat) =>
        {
            const string based = "https://raw.githubusercontent.com/elementh/foscbot/master/assets/based_department.mp4";
            await client.SendVideoAsync(chat, based);
        })
        .WithChances(0.2);
    }
}