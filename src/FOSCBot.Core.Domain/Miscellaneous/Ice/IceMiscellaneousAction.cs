using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Ice;

public class IceMiscellaneousAction : MessageAction
{
    public IceMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        if (Message.Text?.ToLower().Contains("fucking ice") ?? false)
        {
            return true;
        }
            
        if (RandomProvider.GetThreadRandom().NextDouble() < 0.2d )
        {
            return false;
        }

        return (Message.Text?.ToLower().StartsWith("ice") ?? false)
               || (Message.Text?.ToLower().Contains(" ice ") ?? false)
               || (Message.Text?.ToLower().Contains(" ice?") ?? false)
               || (Message.Text?.ToLower().Contains(" hielo ") ?? false)
               || Message.Sticker?.Emoji == "🥶";
    }
}