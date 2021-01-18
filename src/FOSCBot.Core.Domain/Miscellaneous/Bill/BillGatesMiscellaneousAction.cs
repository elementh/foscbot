using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Bill
{
    public class BillGatesMiscellaneousAction : MessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("bill gates") ?? false;
        }
    }
}