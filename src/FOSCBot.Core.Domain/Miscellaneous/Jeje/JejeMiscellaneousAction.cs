using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Jeje;

public class JejeMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        if (RandomProvider.GetThreadRandom().NextDouble() < 0.7d )
        {
            return false;
        }

        return (Message.Text?.ToLower().Contains("je ") ?? false) ||
               (Message.Text?.ToLower().Contains(" je") ?? false) ||
               (Message.Text?.ToLower().Contains(" je ") ?? false) ||
               (Message.Text?.ToLower().Contains("jeje") ?? false) ||
               (Message.Text?.ToLower().Equals("je") ?? false) ||
               (Message.Text?.ToLower().Contains("je je") ?? false);
    }
}