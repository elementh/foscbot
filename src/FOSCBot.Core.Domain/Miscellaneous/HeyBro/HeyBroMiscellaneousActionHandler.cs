using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Domain.Miscellaneous.HeyBro
{
    public class HeyBroMiscellaneousActionHandler : ActionHandler<HeyBroMiscellaneousAction>
    {
        public HeyBroMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(HeyBroMiscellaneousAction request, CancellationToken cancellationToken)
        {
            var bytes = Convert.FromBase64String(CoreResources.HeyBroImage);
            await using (var stream = await new StreamContent(new MemoryStream(bytes)).ReadAsStreamAsync())
            {
                await Ctx.Client.SendPhotoAsync(Ctx.GetTelegramChat(), new InputMedia(stream, "heybroniced.jpg"), 
                    cancellationToken: cancellationToken);
            }

            return Unit.Value;
        }
    }
}