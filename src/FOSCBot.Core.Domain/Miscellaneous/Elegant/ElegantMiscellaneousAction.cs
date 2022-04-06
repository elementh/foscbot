using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Elegant;

public class ElegantMiscellaneousAction : MessageAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        return  (ctx.Update.Message?.Text?.ToLower().Contains("elegant") ?? false) &&
                (!ctx.Update.Message?.Text?.ToLower().ContainsUrl() ?? false);
    }
}