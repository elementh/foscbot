// using FOSCBot.Common.Helper;
// using FOSCBot.Core.Old.Resources;
// using Navigator.Actions;
// using Navigator.Context;
// using Navigator.Providers.Telegram;
// using Telegram.Bot;
//
// namespace FOSCBot.Core.Old.Miscellaneous.Torture;
//
// public class TortureMiscellaneousActionHandler : ActionHandler<TortureMiscellaneousAction>
// {
//     public TortureMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override async Task<Status> Handle(TortureMiscellaneousAction action, CancellationToken cancellationToken)
//     {
//         var choice = RandomProvider.GetThreadRandom().Next(0, 4);
//         switch (choice)
//         {
//             case 0:
//                 await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.CbtExplanation, cancellationToken: cancellationToken);
//                 break;
//             case 1:
//                 await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, "And make it snappy", cancellationToken: cancellationToken);
//                 await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.Conke, cancellationToken: cancellationToken);
//                 break;
//             case 2:
//                 await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.MegatronCbtImmediate, cancellationToken: cancellationToken);
//                 break;
//             case 3:
//                 await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.MegatronCbtExperience, cancellationToken: cancellationToken);
//                 break;
//         }
//             
//         return Success();
//     }
// }