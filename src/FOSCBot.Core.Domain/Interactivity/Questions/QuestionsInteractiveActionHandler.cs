using FOSCBot.Common.Helper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Interactivity.Questions;

public class QuestionsInteractiveActionHandler : ActionHandler<QuestionsInteractiveAction>
{
    private readonly IDistributedCache _distributedCache;

    public QuestionsInteractiveActionHandler(INavigatorContextAccessor navigatorContextAccessor, IDistributedCache distributedCache) : base(navigatorContextAccessor)
    {
        _distributedCache = distributedCache;
    }

    public override async Task<Status> Handle(QuestionsInteractiveAction action, CancellationToken cancellationToken)
    {
        string response;
        var cacheKey = $"_{nameof(QuestionsInteractiveActionHandler)}_{NavigatorContext.GetTelegramChat().Id}";

        var cacheValue = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);

        if (int.TryParse(cacheValue, out var questionsAsked))
        {
            response = questionsAsked switch
            {
                > 10 => QuestionsInteractiveResponseData.OutOfMindResponses.GetRandomFromList(),
                > 3 => QuestionsInteractiveResponseData.DramaticResponses.GetRandomFromList(),
                _ => QuestionsInteractiveResponseData.ChillResponses.GetRandomFromList()
            };
        }
        else
        {
            response = QuestionsInteractiveResponseData.ChillResponses.GetRandomFromList();
        }

        await _distributedCache.SetStringAsync(cacheKey, $"{questionsAsked + 1}",
            new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(30)), cancellationToken);
        
        if (response.IsSticker())
        {
            await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, response, cancellationToken: cancellationToken);
        }
        else
        {
            await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, response, cancellationToken: cancellationToken);
        }

        return Success();
    }
}