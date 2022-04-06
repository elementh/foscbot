using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Domain.Miscellaneous.Ligma;

public class LigmaMiscellaneousActionHandler : ActionHandler<LigmaMiscellaneousAction>
{
    private readonly string LIGMA_TEXT = "ligma balls";
    public LigmaMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(LigmaMiscellaneousAction request, CancellationToken cancellationToken)
    {
        if (RandomProvider.GetThreadRandom().NextDouble() >= 0.75)
        {
            var bytes = Convert.FromBase64String(CoreResources.LigmaHardAudio);
            await using var stream = await new StreamContent(new MemoryStream(bytes)).ReadAsStreamAsync();
                
            await Ctx.Client.SendVoiceAsync(Ctx.GetTelegramChat(), new InputMedia(stream, LIGMA_TEXT.ToUpper()), duration: 4, cancellationToken: cancellationToken);
        }
        else
        {
            var bytes = Convert.FromBase64String(CoreResources.LigmaSoftAudio);
            await using var stream = await new StreamContent(new MemoryStream(bytes)).ReadAsStreamAsync();
                
            await Ctx.Client.SendVoiceAsync(Ctx.GetTelegramChat(), new InputMedia(stream, LIGMA_TEXT), duration: 3, cancellationToken: cancellationToken);
        }

        return Unit.Value;
    }
}