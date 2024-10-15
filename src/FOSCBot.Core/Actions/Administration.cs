using FOSCBot.Core.Services;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Navigator.Client;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Actions;

public static class Administration
{
    public static void RegisterAdministration(this BotActionCatalogFactory catalog)
    {
        catalog
            .OnCommand("setunhinged")
            .SetHandler((INavigatorClient client, UnhingedService service, Chat chat) =>
            {
                service.SetUnhinged(chat.Id);

                client.SendTextMessageAsync(chat, "`Now I am become Death, the destroyer of worlds`", parseMode: ParseMode.MarkdownV2);
            })
            .WithChatAction(ChatAction.Typing);

        catalog
            .OnCommand("clear")
            .SetHandler((UnhingedService service, Chat chat) => service.Clear(chat.Id));
    }
}