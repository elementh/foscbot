using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Command.About
{
    public class AboutCommandAction : CommandAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return Command.ToLower() == "/about";
        }
    }
}