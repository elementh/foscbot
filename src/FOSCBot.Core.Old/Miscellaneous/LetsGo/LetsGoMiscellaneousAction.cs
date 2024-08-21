// using Navigator.Context;
// using Navigator.Providers.Telegram.Actions.Messages;
//
// namespace FOSCBot.Core.Old.Miscellaneous.LetsGo;
//
// public class LetsGoMiscellaneousAction : MessageAction
// {
//     public LetsGoMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override bool CanHandleCurrentContext()
//     {
//         return (Message.Text?.ToLower().StartsWith("let's fucking go") ?? false)
//                || (Message.Text?.ToLower().StartsWith("lets fucking go") ?? false)
//                || (Message.Text?.ToLower().StartsWith("let's fuckin go") ?? false)
//                || (Message.Text?.ToLower().StartsWith("lets fuckin go") ?? false);
//     }
// }