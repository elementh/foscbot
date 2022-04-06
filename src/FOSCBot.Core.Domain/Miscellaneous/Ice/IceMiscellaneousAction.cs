using FOSCBot.Common.Helper;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Ice;

public class IceMiscellaneousAction : MessageAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        if (ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("fucking ice") ?? false)
        {
            return true;
        }
            
        if (RandomProvider.GetThreadRandom().NextDouble() < 0.2d )
        {
            return false;
        }

        return (ctx.Update.Message.Text?.ToLower().StartsWith("ice") ?? false)
               || (ctx.Update.Message.Text?.ToLower().Contains(" ice ") ?? false)
               || (ctx.Update.Message.Text?.ToLower().Contains(" ice?") ?? false)
               || (ctx.Update.Message.Text?.ToLower().Contains(" hielo ") ?? false)
               || ctx.GetMessageOrDefault()?.Sticker?.Emoji == "🥶";
    }
}