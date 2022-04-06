using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Domain.Miscellaneous.Ipad;

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