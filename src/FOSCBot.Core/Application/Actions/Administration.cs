using FOSCBot.Common.Persistence;
using FOSCBot.Core.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            .SetHandler(async (INavigatorClient client, IUnhingedService service, Chat chat, IGiphyService gifs) =>
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
            .SetHandler(async (INavigatorClient client, string[] arguments, IUnhingedService service, Chat chat, IGiphyService gifs) =>
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
            .OnCommand("clear")
            .SetHandler(async (INavigatorClient client, IUnhingedService service, Chat chat) =>
            {
                await client.SendMessage(chat, "`Let's not alert your mom. Going back to defaults...`",
                    parseMode: ParseMode.MarkdownV2);

                service.Clear(chat.Id);
            });

        catalog
            .OnCommand("check")
            .SetHandler(async (INavigatorClient client, IFosboDbContext db, IUnhingedService service, Chat chat) =>
            {
                var usersExist = await db.Users.AnyAsync();
                var mode = service.IsUnhinged(chat.Id);
                var prompt = service.GetPrompt(chat.Id);
                var version = Environment.GetEnvironmentVariable("BOT_VERSION") ?? "unset";
                
                await client.SendMessage(chat, $"""
                                                `Db connected: {usersExist}`
                                                `Unhinged: {mode}`
                                                `Prompt: {prompt ?? "None"}`
                                                `Version: {version}` 
                                                """, 
                    parseMode: ParseMode.MarkdownV2);
            });

        catalog
            .OnCommand("authenticate")
            .SetHandler(async (INavigatorClient client, string[] arguments, IAdminAuthService authService,
                ILogger<IAdminAuthService> logger, Chat chat, Message message) =>
            {
                var userId = message.From?.Id;
                if (userId is null)
                {
                    await client.SendMessage(chat, "`Could not identify you.`", parseMode: ParseMode.MarkdownV2);
                    return;
                }

                if (await authService.IsAdminAsync(userId.Value))
                {
                    await client.SendMessage(chat, "`You are already authenticated.`", parseMode: ParseMode.MarkdownV2);
                    return;
                }

                if (arguments.Length == 0)
                {
                    var code = authService.GenerateCode(userId.Value);
                    logger.LogWarning("Auth code for user {UserId}: {Code}", userId.Value, code);
                    await client.SendMessage(chat, "`I left the secret handshake where only the worthy can find it.`",
                        parseMode: ParseMode.MarkdownV2);
                    return;
                }

                if (await authService.VerifyCodeAsync(userId.Value, arguments.First()))
                {
                    await client.SendMessage(chat, "`Authentication successful. You are now a master.`",
                        parseMode: ParseMode.MarkdownV2);
                }
                else
                {
                    await client.SendMessage(chat, "`Nice try impostor. That's not it.`", parseMode: ParseMode.MarkdownV2);
                }
            });

        catalog
            .OnCommand("purgephantoms")
            .SetHandler(async (INavigatorClient client, IAdminAuthService authService,
                IPhantomCommandService phantomService, Chat chat, Message message) =>
            {
                var userId = message.From?.Id;
                if (userId is null || !await authService.IsAdminAsync(userId.Value))
                {
                    await client.SendMessage(chat, "`You are not authorized to do this.`",
                        parseMode: ParseMode.MarkdownV2);
                    return;
                }

                await phantomService.DeleteAllAsync();
                await client.SendMessage(chat, $"`Purged phantom commands globally.`",
                    parseMode: ParseMode.MarkdownV2);
            });
    }
}