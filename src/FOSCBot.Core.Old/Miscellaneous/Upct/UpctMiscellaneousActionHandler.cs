// using FOSCBot.Common.Helper;
// using Navigator.Actions;
// using Navigator.Context;
// using Navigator.Providers.Telegram;
// using Telegram.Bot;
//
// namespace FOSCBot.Core.Old.Miscellaneous.Upct;
//
// public class UpctMiscellaneousActionHandler : ActionHandler<UpctMiscellaneousAction>
// {
//     public UpctMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override async Task<Status> Handle(UpctMiscellaneousAction action, CancellationToken cancellationToken)
//     {
//         var stickerString = RandomProvider.GetThreadRandom().NextDouble() > 0.2d
//             ? "CAACAgQAAxkBAAJNW16eEHOauvBkLuaD-jL95s86vn2qAAJuAwACmOejAAEys6bCdTOD7RgE"
//             : "CAACAgQAAxkBAAJNXV6eEJLQHwl-8el7YOYYJUF9l8ymAAJZAgACkNStBjfoiv3ywvd8GAQ";
//         await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, stickerString, cancellationToken: cancellationToken);
//
//         if (RandomProvider.GetThreadRandom().NextDouble() > 0.8d)
//             await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, "cAmPuS dE eXcElEnCiA iNtErNaCiOnAl", cancellationToken: cancellationToken);
//
//         return Success();
//     }
// }