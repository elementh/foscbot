using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Command.Coronashark;

public class CoronasharkCommandAction : CommandAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        return Command.ToLower() == "/coronashark";
    }
}