using Navigator.Context;
using Navigator.Extensions.Cooldown;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Old.Miscellaneous.Dineros;

[Cooldown(Seconds = 5 * 60)]
public class DinerosMiscellaneousAction : MessageAction
{
    public DinerosMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }
    
    public override bool CanHandleCurrentContext()
    {
        return (Message.Text?.ToLower().Contains("pobres") ?? false) 
               || (Message.Text?.ToLower().Contains("tesla") ?? false)
               || (Message.Text?.ToLower().Contains("dineros") ?? false);
    }
}