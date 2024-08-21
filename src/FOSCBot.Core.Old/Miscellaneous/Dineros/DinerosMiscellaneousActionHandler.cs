// using FOSCBot.Core.Old.Resources;
// using Navigator.Actions;
// using Navigator.Context;
// using Navigator.Providers.Telegram;
// using Telegram.Bot;
//
// namespace FOSCBot.Core.Old.Miscellaneous.Dineros;
//
// public class DinerosMiscellaneousActionHandler : ActionHandler<DinerosMiscellaneousAction>
// {
//     public DinerosMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override async Task<Status> Handle(DinerosMiscellaneousAction action, CancellationToken cancellationToken)
//     {
//         await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.Dineros, cancellationToken: cancellationToken);
//
//         return Success();
//     }
// }