using FOSCBot.Infrastructure.Contract.Service;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

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

    public override async Task<Status> Handle(MataloCommandAction request, CancellationToken cancellationToken)
    {
        if (request.ReplyToMessageId.HasValue)
        {
            var insult = await _insultService.GetInsult(cancellationToken);

            if (!string.IsNullOrWhiteSpace(insult))
            {
                await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, insult, replyToMessageId: request.ReplyToMessageId.Value,
                    cancellationToken: cancellationToken);
            }
        }
        else
        {
            var answer = await _yesNoService.GetNoImage(cancellationToken);

            if (!string.IsNullOrWhiteSpace(answer))
            {
                await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, answer, replyToMessageId: request.MessageId,
                    cancellationToken: cancellationToken);
            }
        }

        return Success();
    }
}