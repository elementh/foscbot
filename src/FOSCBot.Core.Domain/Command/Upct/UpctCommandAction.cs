using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Command.Upct
{
    public class UpctCommandAction : CommandAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return Command.ToLower() == "/upct";
        }
    }
}
