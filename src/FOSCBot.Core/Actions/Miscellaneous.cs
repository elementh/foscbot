using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Navigator.Client;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Actions;

public static class Miscellaneous
{
    public static void RegisterMiscellaneous(this BotActionCatalogFactory catalog)
    {
        catalog.OnText((string text) => text.Contains("arch", StringComparison.CurrentCultureIgnoreCase),
            async (INavigatorClient client, Chat chat, Message message) =>
            {
                await client.SendTextMessageAsync(chat, "`Btw I run on Arch Linux.`", parseMode: ParseMode.Markdown,
                    replyParameters: message);
            }).WithChances(0.4).WithName("BtwArch");
    }
}