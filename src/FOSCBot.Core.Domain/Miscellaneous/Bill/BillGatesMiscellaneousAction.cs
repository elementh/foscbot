using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Bill;

public class BillGatesMiscellaneousAction : MessageAction
{
    public BillGatesMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return Message.Text?.ToLower().Contains("bill gates") ?? false;
    }
}