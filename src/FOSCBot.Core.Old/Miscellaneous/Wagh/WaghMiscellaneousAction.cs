// using System.Text.RegularExpressions;
// using Navigator.Context;
// using Navigator.Providers.Telegram.Actions.Messages;
//
// namespace FOSCBot.Core.Old.Miscellaneous.Wagh;
//
// public class WaghMiscellaneousAction : MessageAction
// {
//     private static readonly string _pattern = @"^WA*GH$";
//
//     public WaghMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override bool CanHandleCurrentContext()
//     {
//         var regex = new Regex(_pattern);
//
//         return regex.IsMatch(Message.Text ?? string.Empty);
//     }
// }