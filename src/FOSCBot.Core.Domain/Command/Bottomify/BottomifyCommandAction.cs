using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Command.Bottomify;

public class BottomifyCommandAction : CommandAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        return Command.ToLower() == "/bottomify";
    }
}