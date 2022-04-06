using FOSCBot.Common.Helper;
using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.NFT;

public class NFTMiscellaneousAction : MessageAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        return RandomProvider.GetThreadRandom().NextDouble() >= 0.4 && 
               (ctx.Update.Message.Text?.ToLower().Contains("nft") ?? false);
    }
}