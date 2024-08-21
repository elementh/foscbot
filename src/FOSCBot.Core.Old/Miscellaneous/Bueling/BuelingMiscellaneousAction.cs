// using FOSCBot.Common.Helper;
// using Navigator.Context;
// using Navigator.Providers.Telegram.Actions.Messages;
//
// namespace FOSCBot.Core.Old.Miscellaneous.Bueling;
//
// public class BuelingMiscellaneousAction : MessageAction
// {
//     public BuelingMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override bool CanHandleCurrentContext()
//     {
//         return (Message.Text?.ToLower().Contains("vueling") ?? false) ||
//                RandomProvider.GetThreadRandom().NextDouble() > 0.6d &&
//                ((Message.Text?.ToLower().Contains("volar") ?? false) ||
//                 (Message.Text?.ToLower().Contains("avion") ?? false));
//     }
// }