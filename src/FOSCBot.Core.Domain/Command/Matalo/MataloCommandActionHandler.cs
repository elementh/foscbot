using FOSCBot.Infrastructure.Contract.Service;
using MediatR;
using Navigator.Actions;
using Navigator.Context;

namespace FOSCBot.Core.Domain.Command.Matalo;

public class MataloCommandActionHandler : ActionHandler<MataloCommandAction>
{
    private readonly IInsultService _insultService;
    private readonly IYesNoService _yesNoService;

    public MataloCommandActionHandler(INavigatorContext ctx, IInsultService insultService, IYesNoService yesNoService) : base(ctx)
    {
        _insultService = insultService;
        _yesNoService = yesNoService;
    }

    public override async Task<Unit> Handle(MataloCommandAction request, CancellationToken cancellationToken)
    {
        if (request.ReplyToMessageId.HasValue)
        {
            var insult = await _insultService.GetInsult(cancellationToken);

            if (!string.IsNullOrWhiteSpace(insult))
            {
                await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), insult, replyToMessageId: request.ReplyToMessageId.Value,
                    cancellationToken: cancellationToken);
            }
        }
        else
        {
            var answer = await _yesNoService.GetNoImage(cancellationToken);

            if (!string.IsNullOrWhiteSpace(answer))
            {
                await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), answer, replyToMessageId: request.MessageId,
                    cancellationToken: cancellationToken);
            }
        }

        return Unit.Value;
    }
}