// using FOSCBot.Common.Helper;
// using Navigator.Context;
// using Navigator.Providers.Telegram.Actions.Messages;
//
// namespace FOSCBot.Core.Old.Interactivity.Questions;
//
// public class QuestionsInteractiveAction : MessageAction
// {
//     public QuestionsInteractiveAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override bool CanHandleCurrentContext()
//     {
//         return NavigatorContextAccessor.NavigatorContext.IsBotMentioned() && 
//                !NavigatorContextAccessor.NavigatorContext.IsBotPinged() && 
//                !NavigatorContextAccessor.NavigatorContext.IsBotFlattered() && 
//                !NavigatorContextAccessor.NavigatorContext.IsBotBeingToldBadThings();
//     }
// }