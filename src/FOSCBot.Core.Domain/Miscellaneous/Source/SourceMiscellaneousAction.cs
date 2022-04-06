using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Source;

public class SourceMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return (action.Message.Text?.ToLower().Equals("source?") ?? false)
               || (action.Message.Text?.ToLower().Equals("source") ?? false)
               || (action.Message.Text?.ToLower().Equals("sauce?") ?? false)
               || (action.Message.Text?.ToLower().Equals("sauce") ?? false)
               || (action.Message.Text?.ToLower().Equals("saus?") ?? false)
               || (action.Message.Text?.ToLower().Equals("saus") ?? false);
    }
}