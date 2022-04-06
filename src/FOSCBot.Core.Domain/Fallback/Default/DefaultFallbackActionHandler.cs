using FOSCBot.Common.Helper;
using FOSCBot.Infrastructure.Contract.Service;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Domain.Fallback.Default;

public class DefaultFallbackActionHandler : ActionHandler<DefaultFallbackAction>
{
    private readonly ILipsumService _lipsumService;

    public DefaultFallbackActionHandler(INavigatorContextAccessor navigatorContextAccessor, ILipsumService lipsumService) : base(navigatorContextAccessor)
    {
        _lipsumService = lipsumService;
    }

    public override async Task<Status> Handle(DefaultFallbackAction action, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(action.Message.Text) && Bottomify.IsEncoded(action.Message.Text))
        {
            await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!,
                $"`Fellow humans I have decoded these words of wisdom:` \n_{Bottomify.DecodeString(action.Message.Text)}_",
                ParseMode.Markdown,
                cancellationToken: cancellationToken);
        }
            
        if (RandomProvider.GetThreadRandom().Next(0, 600) < 598)
        {
            return Success();
        }
            
        var sentence = string.Empty;

        var odds = RandomProvider.GetThreadRandom().Next(0, 20);

        if (odds >= 0 && odds < 5)
        {
            sentence = await _lipsumService.GetBacon(cancellationToken: cancellationToken);
        }
        else if (odds >= 5 && odds < 10)
        {
            sentence = await _lipsumService.GetMetaphorSentence(cancellationToken: cancellationToken);
        }
        else if (action.Message.Text?.Split(' ').Length > 3)
        {
            sentence = MockFilter.Apply(action.Message.Text);
        }

        if (!string.IsNullOrWhiteSpace(sentence))
        {
            await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, sentence, ParseMode.Markdown,
                replyToMessageId: action.Message.MessageId, cancellationToken: cancellationToken);
        }

        return Success();
    }
}