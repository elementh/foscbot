// using FOSCBot.Common.Helper;
// using FOSCBot.Core.Old.Resources;
// using Navigator.Actions;
// using Navigator.Context;
// using Navigator.Providers.Telegram;
// using Telegram.Bot;
// using Telegram.Bot.Types;
//
// namespace FOSCBot.Core.Old.Miscellaneous.Ligma;
//
// public class LigmaMiscellaneousActionHandler : ActionHandler<LigmaMiscellaneousAction>
// {
//     private readonly string LIGMA_TEXT = "ligma balls";
//     public LigmaMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override async Task<Status> Handle(LigmaMiscellaneousAction action, CancellationToken cancellationToken)
//     {
//         if (RandomProvider.GetThreadRandom().NextDouble() >= 0.75)
//         {
//             var bytes = Convert.FromBase64String(CoreResources.LigmaHardAudio);
//             await using var stream = await new StreamContent(new MemoryStream(bytes)).ReadAsStreamAsync();
//                 
//             await NavigatorContext.GetTelegramClient().SendVoiceAsync(NavigatorContext.GetTelegramChat()!, new InputMedia(stream, LIGMA_TEXT.ToUpper()), duration: 4, cancellationToken: cancellationToken);
//         }
//         else
//         {
//             var bytes = Convert.FromBase64String(CoreResources.LigmaSoftAudio);
//             await using var stream = await new StreamContent(new MemoryStream(bytes)).ReadAsStreamAsync();
//                 
//             await NavigatorContext.GetTelegramClient().SendVoiceAsync(NavigatorContext.GetTelegramChat()!, new InputMedia(stream, LIGMA_TEXT), duration: 3, cancellationToken: cancellationToken);
//         }
//
//         return Success();
//     }
// }