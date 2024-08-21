using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Old.Miscellaneous.Based;

public class BasedMiscellaneousAction : MessageAction
{
    public BasedMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return RandomProvider.GetThreadRandom().NextDouble() <= 0.2d &&
               (Message.Text?.ToLower().Equals("based") ?? false)
               || (Message.Text?.Equals("BASED") ?? false);
    }
}