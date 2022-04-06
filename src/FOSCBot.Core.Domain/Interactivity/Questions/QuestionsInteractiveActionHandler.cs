using FOSCBot.Common.Helper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Navigator.Actions;
using Navigator.Context;

namespace FOSCBot.Core.Domain.Interactivity.Questions;

public class QuestionsInteractiveActionHandler : ActionHandler<QuestionsInteractiveAction>
{
    private readonly IMemoryCache _memoryCache;
    public QuestionsInteractiveActionHandler(INavigatorContext ctx, IMemoryCache memoryCache) : base(ctx)
    {
        _memoryCache = memoryCache;
    }

    public override async Task<Unit> Handle(QuestionsInteractiveAction request, CancellationToken cancellationToken)
    {
        string response;

        if (_memoryCache.TryGetValue($"_{nameof(QuestionsInteractiveActionHandler)}_{Ctx.GetTelegramChatOrDefault()?.Id}", out int questionsAsked))
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

        _memoryCache.Set($"_{nameof(QuestionsInteractiveActionHandler)}_{Ctx.GetTelegramChatOrDefault()?.Id}", questionsAsked + 1, TimeSpan.FromMinutes(30));
        if (response.IsSticker())
        {
            await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), response, cancellationToken: cancellationToken);
        }
        else
        {
            await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), response, cancellationToken: cancellationToken);
        }

        return Unit.Value;
    }
}