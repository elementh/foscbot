using FOSCBot.Common.Helper;
using Navigator.Abstraction;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Uwu
{
    public class UwuMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return RandomProvider.GetThreadRandom().NextDouble() < 0.3d && (ctx.Update.Message.Text?.ToLower().Contains("uwu") ?? false);
        }
    }
}