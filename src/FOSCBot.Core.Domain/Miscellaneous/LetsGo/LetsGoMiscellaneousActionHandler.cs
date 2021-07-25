using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Common.Helper;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.LetsGo
{
    public class LetsGoMiscellaneousActionHandler : ActionHandler<LetsGoMiscellaneousAction>
    {
        public LetsGoMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(LetsGoMiscellaneousAction request, CancellationToken cancellationToken)
        {
            var stickerList = LetsGoHelper.LetsGoStickers;
            var randomSticker = stickerList[RandomProvider.GetThreadRandom().Next(0, stickerList.Length)];
            
            await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), randomSticker, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}