using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.No;

public class NoMiscellaneousAction : MessageAction
{
    public NoMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        if (RandomProvider.GetThreadRandom().NextDouble() < 0.5d )
        {
            return false;
        }

        return (Message.Text?.Equals("NO") ?? false) ||
               (Message.Text?.Equals("NOPE") ?? false);
    }
}