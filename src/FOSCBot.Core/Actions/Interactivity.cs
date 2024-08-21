using FOSCBot.Core.Helpers;
using Incremental.Common.Random;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Actions;

public static class Interactivity
{
    public static void RegisterInteractivity(this BotActionCatalogFactory catalog)
    {
        catalog.OnMessage((Update update) =>
        {
            return update.IsBotQuotedOrMentioned() && update.IsBotBeingToldBadThings();
        }, async (ITelegramBotClient client, Chat chat) =>
        {
            string[] reactions =
            [
                "Sowwry uwu",
                "Perdoooooooon",
                "... :(",
                "Habla con mis dueños para que me arreglen òwó",
                "Acho que no es mi culpa, me programaron así"
            ];

            var randomReaction = reactions[RandomProvider.GetThreadRandom()!.Next(0, reactions.Length)];

            await client.SendTextMessageAsync(chat, randomReaction);
        });
    }
}