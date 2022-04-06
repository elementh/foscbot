using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Bueling;

public class BuelingMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return (action.Message.Text?.ToLower().Contains("vueling") ?? false) ||
               RandomProvider.GetThreadRandom().NextDouble() > 0.6d &&
               ((action.Message.Text?.ToLower().Contains("volar") ?? false) ||
                (action.Message.Text?.ToLower().Contains("avion") ?? false));
    }
}