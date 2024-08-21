// using FOSCBot.Common.Helper;
// using FOSCBot.Core.Old.Resources;
// using Navigator.Actions;
// using Navigator.Context;
// using Navigator.Providers.Telegram;
// using Telegram.Bot;
//
// namespace FOSCBot.Core.Old.Miscellaneous.Traktor;
//
// public class TraktorMiscellaneousActionHandler : ActionHandler<TraktorMiscellaneousAction>
// {
//     public TraktorMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override async Task<Status> Handle(TraktorMiscellaneousAction action, CancellationToken cancellationToken)
//     {
//         if (RandomProvider.GetThreadRandom().NextDouble() <= 0.2d)
//             await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.BuenoFlipao, cancellationToken: cancellationToken);
//         else
//             await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.Traktor, cancellationToken: cancellationToken);
//
//         return Success();
//     }
// }