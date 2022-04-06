using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Source;

public class SourceMiscellaneousActionHandler : ActionHandler<SourceMiscellaneousAction>
{
    public SourceMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(SourceMiscellaneousAction request, CancellationToken cancellationToken)
    {
        if (RandomProvider.GetThreadRandom().NextDouble() <= 0.5d)
            await Ctx.Client.SendPhotoAsync(Ctx.GetTelegramChat(), CoreLinks.Source, cancellationToken: cancellationToken, replyToMessageId: request.MessageId);
        else 
            await Ctx.Client.SendPhotoAsync(Ctx.GetTelegramChat(), CoreLinks.SourceChad, cancellationToken: cancellationToken, replyToMessageId: request.MessageId);

        return Unit.Value;
    }
}