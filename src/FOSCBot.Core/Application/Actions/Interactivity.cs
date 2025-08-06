using FOSCBot.Core.Common;
using Incremental.Common.Random;
using Navigator.Abstractions.Client;
using Navigator.Actions.Builder.Extensions;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Application.Actions;

public static class Interactivity
{
    public static void RegisterInteractivity(this BotActionCatalogFactory catalog)
    {
        catalog
            .OnMessage((Update update) => update.IsBotQuotedOrMentioned() && update.IsBotBeingToldBadThings(),
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

                    await client.SendMessage(chat, randomReaction);
                })
            .WithName("Interactivity.BadBot");

        // catalog
        //     .OnMessage((Update update) => update.IsBotQuotedOrMentioned() && update.IsBotFlattered())
        //     .SetHandler(async (INavigatorClient client, Chat chat, Message message, IMemoryCache cache, ILlamaService llm) =>
        //     {
        //         var replyParameters = default(ReplyParameters);
        //
        //         if (message.ReplyToMessage is not null) replyParameters = message.ReplyToMessage;
        //
        //         var choice = RandomProvider.GetThreadRandom()!.Next(0, 20);
        //         switch (choice)
        //         {
        //             case 0:
        //                 await client.SendMessage(chat, "De nada hermozo 😘", replyParameters: replyParameters);
        //                 break;
        //             case 1:
        //                 // Smiling rani 
        //                 await client.SendStickerAsync(chat, "CAACAgIAAxkBAAEDJMNhdZKneWmWSMJ-5BOOyTK5y4dRpgACCgEAAjDUnRFWVFdpxm65byEE",
        //                     replyParameters: replyParameters);
        //                 break;
        //             case 2:
        //                 // Moon smiling broken 
        //                 await client.SendStickerAsync(chat, "CAACAgIAAxkBAAEDJMlhdZQmchXArRkMCRchHWpgPNLZfgACQQoAAiqWeEhXs1wuuE0lniEE",
        //                     replyParameters: replyParameters);
        //                 break;
        //             case 3:
        //                 // Me aburris tio
        //                 await client.SendStickerAsync(chat, "CAACAgQAAxkBAAEDJMthdZQwLAIyUcECwynw-TuPe_87fAACUgMAApjnowABWVTvcB6NosQhBA",
        //                     replyParameters: replyParameters);
        //                 break;
        //             case 4:
        //                 // P4 Arch broken
        //                 await client.SendStickerAsync(chat, "CAACAgQAAxkBAAEDJM1hdZU5WpnzPHDOqI1SLIc5oZuz9gACWwIAApDUrQYyy_1Go-xzYiEE",
        //                     replyParameters: replyParameters);
        //                 break;
        //             case 5:
        //                 // Croco nice
        //                 await client.SendStickerAsync(chat, "CAACAgIAAxkBAAEDJNFhdZYD0vurwr7VikMz-SbM0TDhSgACLgkAAhhC7ghmx6Iwr7yx9CEE",
        //                     replyParameters: replyParameters);
        //                 break;
        //             case 6:
        //                 // like
        //                 await client.SendMessage(chat, "Dale a like y suscribete", replyParameters: replyParameters);
        //                 break;
        //             case 7:
        //                 // ram
        //                 await client.SendMessage(chat, "Me alegro de poder ayudar. Oye, ¿te sobra un stick de ram?",
        //                     replyParameters: replyParameters);
        //                 break;
        //             default:
        //                 // Llama
        //                 var response = await llm.GetResponse([message.Text ?? "Thank you very much @foscbot"],
        //                     default);
        //
        //                 if (!string.IsNullOrWhiteSpace(response))
        //                     await client.SendMessage(chat, response, replyParameters: replyParameters);
        //                 break;
        //         }
        //
        //         if (cache.Get($"actions:interactivity.Questions:{chat.Id}") is not null)
        //             cache.Remove($"actions:interactivity.Questions:{chat.Id}");
        //     })
        //     .WithName("Interactivity.Flatter");

        catalog
            .OnMessage((Update update) => update.IsBotQuotedOrMentioned() && update.IsBotPinged(),
                async (INavigatorClient client, Chat chat, Message message) =>
                {
                    var delay = DateTime.UtcNow - message.Date;

                    switch (delay.TotalSeconds)
                    {
                        case < 0:
                            await client.SendMessage(chat, $"⬛ acho puta u🅱ct arreglad ya el NTP. Delay: {delay.TotalSeconds}s",
                                replyParameters: message);
                            break;
                        case < 12:
                            await client.SendMessage(chat, $"🟩 toy refinisimo bro. Delay: {delay.TotalSeconds}s",
                                replyParameters: message);
                            break;
                        case < 30:
                            await client.SendMessage(chat, $"🟧 toy F bro. Delay: {delay.TotalSeconds}s",
                                replyParameters: message);
                            break;
                        default:
                            await client.SendMessage(chat, $"🟥 toy joya sosio arreglame ya por dio. Delay: {delay.TotalSeconds}s",
                                replyParameters: message);
                            break;
                    }
                })
            .WithName("Interactivity.Ping");

        // catalog
        //     .OnMessage((Update update) =>
        //         update.IsBotMentioned() && !update.IsBotPinged() && !update.IsBotFlattered() && !update.IsBotBeingToldBadThings())
        //     .SetHandler(async (INavigatorClient client, Chat chat, Message message, IMemoryCache cache, ILlamaService llm) =>
        //     {
        //         string response;
        //         var cacheKey = $"actions:interactivity.Questions:{chat.Id}";
        //
        //         var cacheValue = cache.Get<string>(cacheKey);
        //
        //         var odds = RandomProvider.GetThreadRandom()!.Next(0, 20);
        //
        //         if (int.TryParse(cacheValue, out var questionsAsked))
        //         {
        //             if (odds > 10)
        //                 response = await llm.GetResponse(new[] { message.Text! }, default) ??
        //                            QuestionsInteractiveResponseData.ChillResponses.GetRandomFromList();
        //             else
        //                 response = questionsAsked switch
        //                 {
        //                     > 10 => QuestionsInteractiveResponseData.OutOfMindResponses.GetRandomFromList(),
        //                     > 3 => QuestionsInteractiveResponseData.DramaticResponses.GetRandomFromList(),
        //                     _ => QuestionsInteractiveResponseData.ChillResponses.GetRandomFromList()
        //                 };
        //         }
        //         else
        //         {
        //             response = await llm.GetResponse(new[] { message.Text! }, default) ??
        //                        QuestionsInteractiveResponseData.ChillResponses.GetRandomFromList();
        //         }
        //
        //         cache.Set(cacheKey, $"{questionsAsked + 1}", new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(30)));
        //
        //         if (response.IsSticker())
        //             await client.SendStickerAsync(chat, response);
        //         else
        //             await client.SendMessage(chat, response);
        //     })
        //     .WithName("Interactivity.Questions");
    }
}