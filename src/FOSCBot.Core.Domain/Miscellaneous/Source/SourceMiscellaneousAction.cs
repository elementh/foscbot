using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Source;

public class SourceMiscellaneousAction : MessageAction
{
    public SourceMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return (Message.Text?.ToLower().Equals("source?") ?? false)
               || (Message.Text?.ToLower().Equals("source") ?? false)
               || (Message.Text?.ToLower().Equals("sauce?") ?? false)
               || (Message.Text?.ToLower().Equals("sauce") ?? false)
               || (Message.Text?.ToLower().Equals("saus?") ?? false)
               || (Message.Text?.ToLower().Equals("saus") ?? false);
    }
}