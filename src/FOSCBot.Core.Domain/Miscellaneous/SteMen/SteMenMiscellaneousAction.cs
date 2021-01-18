using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;
namespace FOSCBot.Core.Domain.Miscellaneous.SteMen
{
    public class SteMenMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("ste men") ?? false;
        }
    }
}