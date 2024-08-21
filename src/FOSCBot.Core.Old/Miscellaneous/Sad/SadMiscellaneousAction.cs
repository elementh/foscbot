// using FOSCBot.Common.Helper;
// using Navigator.Context;
// using Navigator.Providers.Telegram.Actions.Messages;
//
// namespace FOSCBot.Core.Old.Miscellaneous.Sad;
//
// public class SadMiscellaneousAction : MessageAction
// {
//     public SadMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override bool CanHandleCurrentContext()
//     {
//         if (RandomProvider.GetThreadRandom().NextDouble() < 0.6d)
//         {
//             return false;
//         }
//             
//         return (Message.Text?.ToLower().Equals("sad") ?? false)
//                ||(Message.Text?.ToLower().Contains(" sad ") ?? false)
//                || Message.Sticker?.Emoji is "😔" or "😢" or "😞" or "😭";
//     }
// }