// using FOSCBot.Common.Helper;
// using Navigator.Context;
// using Navigator.Providers.Telegram.Actions.Messages;
//
// namespace FOSCBot.Core.Old.Miscellaneous.BtwArch;
//
// public class BtwArchMiscellaneousAction : MessageAction
// {
//     public BtwArchMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override bool CanHandleCurrentContext()
//     {
//         return RandomProvider.GetThreadRandom().NextDouble() > 0.6d && (Message.Text?.ToLower().Contains("arch") ?? false);
//     }
// }