using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Command.Matalo;

public class MataloCommandAction : CommandAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        return Command.ToLower() == "/matalo";
    }
}