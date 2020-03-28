using FOSCBot.Common.Helper;
using Navigator.Abstraction;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Nice
{
    public class NiceMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return RandomProvider.GetThreadRandom().NextDouble() < 0.7d && (ctx.Update.Message.Text?.Contains("NICE") ?? false);
        }
    }
}