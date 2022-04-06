using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Ree;

public class ReeMiscellaneousAction : MessageAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        return ctx.GetMessageOrDefault()?.Text?.Contains("REE") ?? false;
    }
}