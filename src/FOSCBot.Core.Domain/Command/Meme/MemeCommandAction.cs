using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Command.Quote;

public class MemeCommandAction : CommandAction
{
    public MemeCommandAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return Command.ToLower() == "/meme";
    }
}