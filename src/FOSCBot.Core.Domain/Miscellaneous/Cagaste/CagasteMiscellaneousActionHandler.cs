using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Cagaste;

public class CagasteMiscellaneousActionHandler : ActionHandler<CagasteMiscellaneousAction>
{
    public CagasteMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(CagasteMiscellaneousAction request, CancellationToken cancellationToken)
    {
        if (RandomProvider.GetThreadRandom().NextDouble() <= 0.5d)
            await Ctx.Client.SendPhotoAsync(Ctx.GetTelegramChat(), CoreLinks.CagasteGoku, cancellationToken: cancellationToken);
        else
            await Ctx.Client.SendPhotoAsync(Ctx.GetTelegramChat(), CoreLinks.CagasteShark, cancellationToken: cancellationToken);

        return Unit.Value;
    }
}