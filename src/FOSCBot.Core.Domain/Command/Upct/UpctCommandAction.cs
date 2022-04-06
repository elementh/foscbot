using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Command.Upct;

public class UpctCommandAction : CommandAction
{
    public override bool CanHandleCurrentContext()
    {
        return Command.ToLower() == "/upct";
    }
}