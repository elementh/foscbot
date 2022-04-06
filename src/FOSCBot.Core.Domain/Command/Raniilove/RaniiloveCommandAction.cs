using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Command.Raniilove;

public class RaniiloveCommandAction : CommandAction
{
    public override bool CanHandleCurrentContext()
    {
        return Command.ToLower() == "/raniilove";
    }
}