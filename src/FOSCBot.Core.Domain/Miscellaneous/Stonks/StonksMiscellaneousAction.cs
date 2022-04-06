using FOSCBot.Common.Helper;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Stonks;

public class StonksMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return RandomProvider.GetThreadRandom().NextDouble() > 0.6d && (ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("stonks") ?? false);
    }
}