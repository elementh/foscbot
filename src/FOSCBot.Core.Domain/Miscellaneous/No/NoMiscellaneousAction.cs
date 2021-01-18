using FOSCBot.Common.Helper;
using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.No
{
    public class NoMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            if (RandomProvider.GetThreadRandom().NextDouble() < 0.5d )
            {
                return false;
            }

            return (ctx.Update.Message.Text?.Equals("NO") ?? false) ||
                   (ctx.Update.Message.Text?.Equals("NOPE") ?? false);
        }
    }
}