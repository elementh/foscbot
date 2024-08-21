// using Microsoft.Extensions.Configuration;
// using Navigator.Actions;
// using Navigator.Context;
// using Navigator.Providers.Telegram;
// using Telegram.Bot;
// using Telegram.Bot.Types.Enums;
//
// namespace FOSCBot.Core.Old.Command.Version;
//
// public class VersionActionHandler : ActionHandler<VersionAction>
// {
//     private readonly IConfiguration _configuration;
//     public VersionActionHandler(INavigatorContextAccessor navigatorContextAccessor, IConfiguration configuration) : base(navigatorContextAccessor)
//     {
//         _configuration = configuration;
//     }
//
//     public override async Task<Status> Handle(VersionAction action, CancellationToken cancellationToken)
//     {
//         var text = $"`My current bot version is: {_configuration["BOT_VERSION"]}`";
//         
//         await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, text, ParseMode.Markdown, cancellationToken: cancellationToken);
//
//         return Success();
//     }
// }