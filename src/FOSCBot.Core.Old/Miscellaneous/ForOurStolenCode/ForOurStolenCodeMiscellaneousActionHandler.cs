// using FOSCBot.Core.Old.Resources;
// using Navigator.Actions;
// using Navigator.Context;
// using Navigator.Providers.Telegram;
// using Telegram.Bot;
//
// namespace FOSCBot.Core.Old.Miscellaneous.ForOurStolenCode;
//
// public class ForOurStolenCodeMiscellaneousActionHandler : ActionHandler<ForOurStolenCodeMiscellaneousAction>
// {
//     public ForOurStolenCodeMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override async Task<Status> Handle(ForOurStolenCodeMiscellaneousAction action, CancellationToken cancellationToken)
//     {
//         await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.Orks, cancellationToken: cancellationToken);
//             
//         return Success();
//     }
// }