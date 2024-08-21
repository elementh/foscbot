// using Navigator.Context;
// using Navigator.Providers.Telegram.Actions.Messages;
//
// namespace FOSCBot.Core.Old.Miscellaneous.Succ;
//
// public class SuccMiscellaneousAction : MessageAction
// {
//     public SuccMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override bool CanHandleCurrentContext()
//     {
//         return (Message.Text?.Equals("SUCC") ?? false) || (Message.Text?.Equals("SAC") ?? false);
//     }
// }