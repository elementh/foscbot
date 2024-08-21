using FOSCBot.Core.Resources;
using Incremental.Common.Random;
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
            const string about = """
                                 More info about FOSC at [fosc.space](https://fosc.space).
                                 Check out my source code at [foscbot](https://github.com/elementh/foscbot).

                                 Made with ❤️ and gratitude by [Lucas Maximiliano Marino](https://lucasmarino.me) & many others for all the cool people at [FOSC](https://fosc.space). 
                                 """;
            
            await client.SendTextMessageAsync(chat, about, parseMode: ParseMode.Markdown);
        });
    }
}