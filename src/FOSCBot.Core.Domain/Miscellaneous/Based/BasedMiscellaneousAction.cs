using FOSCBot.Common.Helper;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Based;

public class BasedMiscellaneousAction : MessageAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        return RandomProvider.GetThreadRandom().NextDouble() <= 0.2d &&
               (ctx.GetMessageOrDefault()?.Text?.ToLower().Equals("based") ?? false)
               || (ctx.GetMessageOrDefault()?.Text?.Equals("BASED") ?? false);
    }
}