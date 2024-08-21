using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Old.Miscellaneous.Elegant;

public class ElegantMiscellaneousAction : MessageAction
{
    public ElegantMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return  (Message.Text?.ToLower().Contains("elegant") ?? false) &&
                (!Message.Text?.ToLower().ContainsUrl() ?? false);
    }
}