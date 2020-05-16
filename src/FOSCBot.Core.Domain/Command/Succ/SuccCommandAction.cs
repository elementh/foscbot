using Navigator.Abstraction;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Command.Succ
{
    public class SuccCommandAction : CommandAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return Command.ToLower() == "/succ";
        }
    }
}