// using Navigator.Context;
// using Navigator.Providers.Telegram.Actions.Messages;
//
// namespace FOSCBot.Core.Old.Miscellaneous.Ree;
//
// public class ReeMiscellaneousAction : MessageAction
// {
//     public ReeMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override bool CanHandleCurrentContext()
//     {
//         return Message.Text?.Contains("REE") ?? false;
//     }
// }