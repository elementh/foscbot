using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Elon;

public class ElonMuskMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return (action.Message.Text?.ToLower().Contains("elon musk") ?? false) ||
               (action.Message.Text?.ToLower().StartsWith("elon") ?? false) ||
               (action.Message.Text?.ToLower().Contains("elon ") ?? false) ||
               (action.Message.Text?.ToLower().EndsWith(" elon") ?? false) ||
               (action.Message.Text?.ToLower().StartsWith("musk") ?? false) ||
               (action.Message.Text?.ToLower().Contains("musk ") ?? false) ||
               (action.Message.Text?.ToLower().EndsWith(" musk") ?? false);
    }
}