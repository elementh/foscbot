using FOSCBot.Common.Helper;
using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Yes
{
    public class YesMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            if (RandomProvider.GetThreadRandom().NextDouble() < 0.5d )
            {
                return false;
            }

            return (ctx.Update.Message.Text?.Equals("YES") ?? false) ||
                   (ctx.Update.Message.Text?.Equals("SI") ?? false);
        }
    }
}