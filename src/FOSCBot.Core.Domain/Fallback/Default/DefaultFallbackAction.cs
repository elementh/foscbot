using FOSCBot.Common.Helper;
using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Fallback.Default
{
    public class DefaultFallbackAction : MessageAction
    {
        public new int Order = 1100;
        
        public override bool CanHandle(INavigatorContext ctx)
        {
            return RandomProvider.GetThreadRandom().Next(0, 600) > 599;
        }
    }
}