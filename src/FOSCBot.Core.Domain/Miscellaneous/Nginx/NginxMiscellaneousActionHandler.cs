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

namespace FOSCBot.Core.Domain.Miscellaneous.Nginx
{
    public class NginxMiscellaneousActionHandler : ActionHandler<NginxMiscellaneousAction>
    {
        public NginxMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(NginxMiscellaneousAction request, CancellationToken cancellationToken)
        {
            var bytes = Convert.FromBase64String(CoreResources.NginxImage);
            await using (var stream = await new StreamContent(new MemoryStream(bytes)).ReadAsStreamAsync())
            {
                await Ctx.Client.SendPhotoAsync(Ctx.GetTelegramChat(), new InputMedia(stream, "nginx.jpg"), 
                    cancellationToken: cancellationToken);
            }

            return Unit.Value;
        }
    }
}