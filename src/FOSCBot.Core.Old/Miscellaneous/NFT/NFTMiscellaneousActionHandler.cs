// using FOSCBot.Common.Helper;
// using FOSCBot.Core.Old.Resources;
// using Navigator.Actions;
// using Navigator.Context;
// using Navigator.Providers.Telegram;
// using Telegram.Bot;
//
// namespace FOSCBot.Core.Old.Miscellaneous.NFT;
//
// public class NFTMiscellaneousActionHandler : ActionHandler<NFTMiscellaneousAction>
// {
//     public NFTMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
//     {
//     }
//
//     public override async Task<Status> Handle(NFTMiscellaneousAction action, CancellationToken cancellationToken)
//     {
//         var nft = new List<string>
//         {
//             CoreLinks.NFT,
//             CoreLinks.NFToad,
//             CoreLinks.NFTractor,
//             CoreLinks.NFTattoo,
//             CoreLinks.NFTu
//         }.GetRandomFromList();
//             
//         await NavigatorContext.GetTelegramClient().SendPhotoAsync(NavigatorContext.GetTelegramChat()!, nft, cancellationToken: cancellationToken);
//
//         return Success();
//     }
// }