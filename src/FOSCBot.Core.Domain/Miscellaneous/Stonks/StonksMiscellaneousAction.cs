using FOSCBot.Common.Helper;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Stonks;

public class StonksMiscellaneousAction : MessageAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        return RandomProvider.GetThreadRandom().NextDouble() > 0.6d && (ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("stonks") ?? false);
    }
}