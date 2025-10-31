using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Application.Abstractions;

public interface IAgentService
{
    [Experimental("SKEXP0001")]
    Task<string?> ProcessMessage(Chat chat, Message message);
}

