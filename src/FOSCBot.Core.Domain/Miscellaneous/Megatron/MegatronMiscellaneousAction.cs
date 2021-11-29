using FOSCBot.Common.Helper;
using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Megatron
{
    public class MegatronMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return  (ctx.Update.Message?.Text?.ToLower().Contains("megatron") ?? false) &&
                    (!ctx.Update.Message?.Text?.ToLower().ContainsUrl() ?? false);
        }
    }
}