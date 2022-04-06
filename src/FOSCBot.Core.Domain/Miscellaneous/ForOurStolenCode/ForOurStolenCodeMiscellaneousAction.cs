using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.ForOurStolenCode;

public class ForOurStolenCodeMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return RandomProvider.GetThreadRandom().NextDouble() < 0.8d && (ctx.Update.Message.Text?.ToLower().Contains("for our stolen code") ?? false);
    }
}