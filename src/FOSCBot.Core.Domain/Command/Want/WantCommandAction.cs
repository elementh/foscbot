using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Command.Want;

public class WantCommandAction : CommandAction
{
    public override bool CanHandleCurrentContext()
    {
        return Command.ToLower() == "/want";
    }
}