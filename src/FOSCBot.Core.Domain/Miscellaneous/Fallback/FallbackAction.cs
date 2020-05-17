using FOSCBot.Common.Helper;
using Navigator.Abstraction;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Fallback
{
    public class FallbackAction : MessageAction
    {
        public new int Order = 1100;
        
        public override bool CanHandle(INavigatorContext ctx)
        {
            return RandomProvider.GetThreadRandom().Next(0, 600) > 599;
        }
    }
}