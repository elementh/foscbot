using FOSCBot.Core.Services;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Actions;

public static class Administration
{
    public static void RegisterAdministration(this BotActionCatalogFactory catalog)
    {
        catalog
            .OnCommand("/setunhinged")
            .SetHandler((UnhingedService service, Chat chat) => service.SetUnhinged(chat.Id))
            .WithChatAction(ChatAction.Typing);

        catalog
            .OnCommand("/clear")
            .SetHandler((UnhingedService service, Chat chat) => service.Clear(chat.Id));
    }
}