using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Uwu;

public class UwuMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return RandomProvider.GetThreadRandom().NextDouble() < 0.3d && (ctx.Update.Message.Text?.ToLower().Contains("uwu") ?? false);
    }
}