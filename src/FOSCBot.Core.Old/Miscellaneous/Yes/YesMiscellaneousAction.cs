using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Old.Miscellaneous.Yes;

public class YesMiscellaneousAction : MessageAction
{
    public YesMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        if (RandomProvider.GetThreadRandom().NextDouble() < 0.5d )
        {
            return false;
        }

        return (Message.Text?.Equals("YES") ?? false) ||
               (Message.Text?.Equals("SI") ?? false);
    }
}