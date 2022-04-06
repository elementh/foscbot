using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Command.Coronashark;

public class CoronasharkCommandAction : CommandAction
{
    public override bool CanHandleCurrentContext()
    {
        return Command.ToLower() == "/coronashark";
    }
}