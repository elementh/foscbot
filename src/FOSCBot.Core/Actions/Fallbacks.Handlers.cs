using System.Diagnostics.CodeAnalysis;
using FOSCBot.Core.Helpers;
using FOSCBot.Core.Services;
using Microsoft.Extensions.Logging;
using Navigator.Client;
using Navigator.Strategy;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Actions;

public static partial class Fallbacks
{
    [Experimental("SKEXP0001")]
    private static async Task CatchAllHandler(INavigatorClient client, Chat chat, Message message, Update update,
        ProbabilityService probabilities, AgentService agentService, ILogger<NavigatorStrategy> logger)
    {
        try
        {
            if (message.Type is not (MessageType.Text or MessageType.Sticker)) return;

            var shouldAnswer = update.IsBotQuotedOrMentioned() || probabilities.GetResult(chat.Id);

            if (shouldAnswer)
            {
                await client.SendChatAction(chat, ChatAction.Typing);

                var response = await agentService.ProcessMessage(chat, message);

                if (response != null)
                {
                    await client.SendMessage(chat.Id, response, parseMode: ParseMode.Markdown);
                    probabilities.Reset(chat.Id);
                }
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to process fallback handler for chat {ChatId}", chat.Id);
        }
    }
}
