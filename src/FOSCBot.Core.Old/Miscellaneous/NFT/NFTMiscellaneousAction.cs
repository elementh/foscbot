using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Old.Miscellaneous.NFT;

public class NFTMiscellaneousAction : MessageAction
{
    public NFTMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override bool CanHandleCurrentContext()
    {
        return RandomProvider.GetThreadRandom().NextDouble() >= 0.4 && 
               (Message.Text?.ToLower().Contains("nft") ?? false);
    }
}