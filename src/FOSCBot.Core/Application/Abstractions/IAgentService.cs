using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Application.Abstractions;

public interface IAgentService
{
    [Experimental("SKEXP0001")]
    Task<string?> ProcessMessage(Chat chat, Message message);

    [Experimental("SKEXP0001")]
    Task<string?> Ask(string question, string username);

    [Experimental("SKEXP0001")]
    Task<string?> CommentOnSergioParadox(string messageText, string username);

    [Experimental("SKEXP0001")]
    Task<string?> ReduceTextLength(string text, string targetLength);
}
