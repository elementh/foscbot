using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Command.P4Cock;

public class P4CockCommandAction : CommandAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        return Command.ToLower() == "/p4cock";
    }
}