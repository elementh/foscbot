using FOSCBot.Common.Persistence;
using FOSCBot.Core.Application.Abstractions;
using FOSCBot.Core.Application.Services;
using Microsoft.EntityFrameworkCore;
using Navigator.Abstractions.Client;
using Navigator.Actions.Builder.Extensions;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Application.Actions;

public static class Administration
{
    public static void RegisterAdministration(this BotActionCatalogFactory catalog)
    {
        catalog
            .OnCommand("setunhinged")
            .SetHandler(async (INavigatorClient client, UnhingedService service, Chat chat, IGiphyService gifs) =>
            {
                service.SetUnhinged(chat.Id);

                await client.SendMessage(chat, "`Now I am become Death, the destroyer of worlds`",
                    parseMode: ParseMode.MarkdownV2);
                var gifUrl = await gifs.GetOneOf(["apocalypse", "bomb", "slaughter", "rage"]);

                if (gifUrl is not null) await client.SendAnimation(chat, new InputFileUrl(gifUrl));
            })
            .WithChatAction(ChatAction.Typing);

        catalog
            .OnCommand("setprompt")
            .SetHandler(async (INavigatorClient client, string[] arguments, UnhingedService service, Chat chat, IGiphyService gifs) =>
            {
                if (arguments.Length == 0)
                {
                    await client.SendMessage(chat, "`I need a prompt BRO`", parseMode: ParseMode.MarkdownV2);
                    return;
                }

                var prompt = string.Join(" ", arguments);

                service.SetPrompt(chat.Id, prompt);

                await client.SendMessage(chat, "`Sure thing boss.`", parseMode: ParseMode.MarkdownV2);
                var gifUrl = await gifs.GetOneOf(["sure thing", "alright", "yes maam", "alright bro", "boss", "ok"]);

                if (gifUrl is not null) await client.SendAnimation(chat, new InputFileUrl(gifUrl));
            })
            .WithChatAction(ChatAction.Typing);

        catalog
            .OnCommand("settemperature")
            .SetHandler(async (INavigatorClient client, string[] arguments, UnhingedService service, Chat chat, IGiphyService gifs) =>
            {
                if (arguments.Length == 0)
                {
                    await client.SendMessage(chat, "`I need a temperature SISTER`", parseMode: ParseMode.MarkdownV2);
                    return;
                }

                if (double.TryParse(arguments.First(), out var temperature))
                {
                    service.SetTemperature(chat.Id, temperature);

                    await client.SendMessage(chat, "`Sure thing SISTER.`", parseMode: ParseMode.MarkdownV2);

                    var gifUrl = temperature switch
                    {
                        > 1 => await gifs.Get("freezing"),
                        >= 0.9 => await gifs.Get("cold"),
                        > 0.6 => await gifs.Get("warm"),
                        _ => await gifs.Get("extreme heat")
                    };

                    if (gifUrl is not null) await client.SendAnimation(chat, new InputFileUrl(gifUrl));
                }
            })
            .WithChatAction(ChatAction.Typing);


        catalog
            .OnCommand("clear")
            .SetHandler(async (INavigatorClient client, UnhingedService service, Chat chat) =>
            {
                await client.SendMessage(chat, "`Let's not alert your mom. Going back to defaults...`",
                    parseMode: ParseMode.MarkdownV2);

                service.Clear(chat.Id);
            });

        catalog
            .OnCommand("check")
            .SetHandler(async (INavigatorClient client, IFosboDbContext db, UnhingedService service, Chat chat) =>
            {
                var usersExist = await db.Users.AnyAsync();
                var mode = service.IsUnhinged(chat.Id);
                var temperature = service.GetTemperature(chat.Id);
                var prompt = service.GetPrompt(chat.Id);
                
                await client.SendMessage(chat, $"""
                                                `Db connected: {usersExist}`
                                                `Unhinged: {mode}`
                                                `Temperature: {temperature ?? 1}`
                                                `Prompt: {prompt ?? "None"}`
                                                """, 
                    parseMode: ParseMode.MarkdownV2);
            });
    }
}