using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Command.Nope;

public class NopeCommandAction : CommandAction
{
    public override bool CanHandleCurrentContext()
    {
        return Command.ToLower() == "/nope";
    }
}