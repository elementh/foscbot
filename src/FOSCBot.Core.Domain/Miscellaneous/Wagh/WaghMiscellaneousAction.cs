using System.Text.RegularExpressions;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Wagh;

public class WaghMiscellaneousAction : MessageAction
{
    private static readonly string _pattern = @"^WA*GH$";
    public override bool CanHandle(INavigatorContext ctx)
    {
        var regex = new Regex(_pattern);

        return regex.IsMatch(ctx.GetMessageOrDefault()?.Text ?? string.Empty);
    }
}