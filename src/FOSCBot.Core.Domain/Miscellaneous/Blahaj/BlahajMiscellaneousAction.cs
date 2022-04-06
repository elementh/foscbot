using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Blahaj;

public class BlahajMiscellaneousAction : MessageAction
{
    public override bool CanHandle(INavigatorContext ctx)
    {
        return ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("blahaj") ?? false;
    }
}