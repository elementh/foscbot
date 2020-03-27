using FOSCBot.Common.Helper;
using Navigator;
using Navigator.Abstraction;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.BtwArch
{
    public class BtwArchMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return RandomProvider.GetThreadRandom().NextDouble() > 0.7d && (ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("arch") ?? false);
        }
    }
}