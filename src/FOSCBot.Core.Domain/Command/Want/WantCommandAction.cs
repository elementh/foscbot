using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Command.Want;

public class WantCommandAction : CommandAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        return Command.ToLower() == "/want";
    }
}