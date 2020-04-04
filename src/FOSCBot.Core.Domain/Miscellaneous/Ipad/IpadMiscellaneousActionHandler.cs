using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Miscellaneous.Uwu;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace FOSCBot.Core.Domain.Miscellaneous.Ipad
{
    public class IpadMiscellaneousActionHandler : ActionHandler<IpadMiscellaneousAction>
    {
        public IpadMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(IpadMiscellaneousAction request, CancellationToken cancellationToken)
        {
            if (RandomProvider.GetThreadRandom().NextDouble() >= 0.5)
            {
                await Ctx.Client.SendPhotoAsync(Ctx.GetTelegramChat(), CoreLinks.Ipad, "tEnGo Un IpAd", cancellationToken: cancellationToken);
            }
            else
            {
                var bytes = Convert.FromBase64String(CoreResources.IpadAudio);
                await using var stream = await new StreamContent(new MemoryStream(bytes)).ReadAsStreamAsync();
                
                await Ctx.Client.SendVoiceAsync(Ctx.GetTelegramChat(), new InputMedia(stream, "ipad"), duration: 5, cancellationToken: cancellationToken);
            }

            return Unit.Value;
        }
    }
}