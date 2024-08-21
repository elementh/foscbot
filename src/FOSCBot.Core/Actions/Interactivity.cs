using FOSCBot.Core.Helpers;
using Incremental.Common.Random;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Navigator.Client;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Actions;

public static class Interactivity
{
    public static void RegisterInteractivity(this BotActionCatalogFactory catalog)
    {
        catalog.OnMessage((Update update) => update.IsBotQuotedOrMentioned() && update.IsBotBeingToldBadThings(),
            async (INavigatorClient client, Chat chat) =>
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

        catalog.OnMessage((Update update) => update.IsBotQuotedOrMentioned() && update.IsBotPinged(),
            async (INavigatorClient client, Chat chat, Message message) =>
            {
                var delay = DateTime.UtcNow - message.Date;

                switch (delay.TotalSeconds)
                {
                    case < 0:
                        await client.SendTextMessageAsync(chat, $"⬛ acho puta u🅱ct arreglad ya el NTP. Delay: {delay.TotalSeconds}s",
                            replyParameters: message);
                        break;
                    case < 12:
                        await client.SendTextMessageAsync(chat, $"🟩 toy refinisimo bro. Delay: {delay.TotalSeconds}s",
                            replyParameters: message);
                        break;
                    case < 30:
                        await client.SendTextMessageAsync(chat, $"🟧 toy F bro. Delay: {delay.TotalSeconds}s", replyParameters: message);
                        break;
                    default:
                        await client.SendTextMessageAsync(chat, $"🟥 toy joya sosio arreglame ya por dio. Delay: {delay.TotalSeconds}s",
                            replyParameters: message);
                        break;
                }
            });
    }
}