using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.ForOurStolenCode;

public class ForOurStolenCodeMiscellaneousAction : MessageAction
{
    public ForOurStolenCodeMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return RandomProvider.GetThreadRandom().NextDouble() < 0.8d && (Message.Text?.ToLower().Contains("for our stolen code") ?? false);
    }
}