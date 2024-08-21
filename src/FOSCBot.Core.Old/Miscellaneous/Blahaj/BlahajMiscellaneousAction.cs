// using Navigator.Context;
// using Navigator.Providers.Telegram.Actions.Messages;
//
// namespace FOSCBot.Core.Old.Miscellaneous.Blahaj;
//
// public class BlahajMiscellaneousAction : MessageAction
// {
//     public BlahajMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override bool CanHandleCurrentContext()
//     {
//         return Message.Text?.ToLower().Contains("blahaj") ?? false;
//     }
// }