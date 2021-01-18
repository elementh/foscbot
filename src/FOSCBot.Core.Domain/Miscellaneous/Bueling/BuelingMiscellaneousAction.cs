using FOSCBot.Common.Helper;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Bueling
{
    public class BuelingMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return (ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("vueling") ?? false) ||
                   RandomProvider.GetThreadRandom().NextDouble() > 0.6d &&
                   ((ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("volar") ?? false) ||
                    (ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("avion") ?? false));
        }
    }
}
