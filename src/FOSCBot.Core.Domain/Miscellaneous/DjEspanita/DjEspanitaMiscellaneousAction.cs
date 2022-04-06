using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.DjEspanita;

public class DjEspanitaMiscellaneousAction : MessageAction
{
    public DjEspanitaMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return RandomProvider.GetThreadRandom().NextDouble() > 0.6d && 
               (Message.Text?.ToLower().Contains("himno de españa") ?? false);
    }
}