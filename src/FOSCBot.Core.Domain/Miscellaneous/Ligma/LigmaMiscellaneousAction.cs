using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Ligma;

public class LigmaMiscellaneousAction : MessageAction
{
    public LigmaMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return (Message.Text?.ToLower().Contains("so sad") ?? false) ||
               (Message.Text?.ToLower().Contains("ligma") ?? false) ||
               (Message.Text?.ToLower().Contains("p4cock") ?? false);
    }
}