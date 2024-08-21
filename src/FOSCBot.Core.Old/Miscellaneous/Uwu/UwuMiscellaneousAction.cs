// using FOSCBot.Common.Helper;
// using Navigator.Context;
// using Navigator.Providers.Telegram.Actions.Messages;
//
// namespace FOSCBot.Core.Old.Miscellaneous.Uwu;
//
// public class UwuMiscellaneousAction : MessageAction
// {
//     public UwuMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override bool CanHandleCurrentContext()
//     {
//         return RandomProvider.GetThreadRandom().NextDouble() < 0.3d && (Message.Text?.ToLower().Contains("uwu") ?? false);
//     }
// }