using FOSCBot.Common.Helper;
using Navigator.Abstraction;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.ForOurStolenCode
{
    public class ForOurStolenCodeMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return RandomProvider.GetThreadRandom().NextDouble() < 0.8d && (ctx.Update.Message.Text?.ToLower().Contains("for our stolen code") ?? false);
        }
    }
}