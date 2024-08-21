using FOSCBot.Core.Resources;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Navigator.Client;
using Navigator.Entities;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Actions;

public static class Commands
{
    public static void RegisterCommands(this BotActionCatalogFactory catalog)
    {
        catalog.OnCommand("about", async (INavigatorClient client, Chat chat) =>
        {
            await client.SendTextMessageAsync(chat, CoreResources.AboutText, parseMode:ParseMode.Markdown);
        });
    }
}