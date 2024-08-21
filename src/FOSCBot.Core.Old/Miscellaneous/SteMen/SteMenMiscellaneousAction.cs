// using Navigator.Context;
// using Navigator.Providers.Telegram.Actions.Messages;
//
// namespace FOSCBot.Core.Old.Miscellaneous.SteMen;
//
// public class SteMenMiscellaneousAction : MessageAction
// {
//     public SteMenMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override bool CanHandleCurrentContext()
//     {
//         return Message.Text?.ToLower().Contains("ste men") ?? false;
//     }
// }