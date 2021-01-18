using FOSCBot.Common.Helper;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.BtwArch
{
    public class BtwArchMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return RandomProvider.GetThreadRandom().NextDouble() > 0.6d && (ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("arch") ?? false);
        }
    }
}