// using Navigator.Context;
// using Navigator.Extensions.Cooldown;
// using Navigator.Providers.Telegram.Actions.Messages;
//
// namespace FOSCBot.Core.Old.Miscellaneous.Ipad;
//
// [Cooldown(Seconds = 15 * 60)]
// public class IpadMiscellaneousAction : MessageAction
// {
//     public IpadMiscellaneousAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override bool CanHandleCurrentContext()
//     {
//         return Message.Text?.ToLower().Contains(" ipad") ?? false;
//     }
// }