using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Command.Boi
{
    public class BoiCommandAction : CommandAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return Command.ToLower() == "/boi";
        }
    }
}