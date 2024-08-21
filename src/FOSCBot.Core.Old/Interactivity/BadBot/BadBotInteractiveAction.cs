// using FOSCBot.Common.Helper;
// using Navigator.Context;
// using Navigator.Providers.Telegram.Actions.Messages;
//
// namespace FOSCBot.Core.Old.Interactivity.BadBot;
//
// public class BadBotInteractiveAction : MessageAction
// {
//     public BadBotInteractiveAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override bool CanHandleCurrentContext()
//     {
//         return NavigatorContextAccessor.NavigatorContext.IsBotQuotedOrMentioned() 
//                && NavigatorContextAccessor.NavigatorContext.IsBotBeingToldBadThings();
//     }
// }