using FOSCBot.Common.Helper;
using Navigator.Abstraction;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.HeyBro
{
    public class HeyBroMiscellaneousAction : MessageAction
    {
        protected static readonly string StickerId = "DAAKY56MAAQspeiAK-9YiGAQ";
        public override bool CanHandle(INavigatorContext ctx)
        {
            return RandomProvider.GetThreadRandom().NextDouble() < 0.7d && (ctx.Update.Message.Sticker?.FileId.EndsWith(StickerId) ?? false);
        }
    }
}