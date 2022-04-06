using FOSCBot.Infrastructure.Contract.Service;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Command.Matalo;

public class MataloCommandActionHandler : ActionHandler<MataloCommandAction>
{
    private readonly IInsultService _insultService;
    private readonly IYesNoService _yesNoService;

    public MataloCommandActionHandler(INavigatorContextAccessor navigatorContextAccessor, IInsultService insultService, IYesNoService yesNoService) : base(navigatorContextAccessor)
    {
        _insultService = insultService;
        _yesNoService = yesNoService;
    }

    public override async Task<Status> Handle(MataloCommandAction action, CancellationToken cancellationToken)
    {
        if (action.IsReply)
        {
            var insult = await _insultService.GetInsult(cancellationToken);

            if (!string.IsNullOrWhiteSpace(insult))
            {
                await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, insult, replyToMessageId: action.Message.ReplyToMessage?.MessageId,
                    cancellationToken: cancellationToken);
            }
        }
        else
        {
            var answer = await _yesNoService.GetNoImage(cancellationToken);

            if (!string.IsNullOrWhiteSpace(answer))
            {
                await NavigatorContext.GetTelegramClient().SendVideoAsync(NavigatorContext.GetTelegramChat()!, answer, replyToMessageId: action.Message.MessageId,
                    cancellationToken: cancellationToken);
            }
        }

        return Success();
    }
}