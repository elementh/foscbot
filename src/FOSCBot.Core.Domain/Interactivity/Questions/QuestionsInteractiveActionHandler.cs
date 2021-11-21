using System;
using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Common.Helper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Interactivity.Questions
{
    public class QuestionsInteractiveActionHandler : ActionHandler<QuestionsInteractiveAction>
    {
        private readonly IMemoryCache _memoryCache;
        public QuestionsInteractiveActionHandler(INavigatorContext ctx, IMemoryCache memoryCache) : base(ctx)
        {
            _memoryCache = memoryCache;
        }

        public override async Task<Unit> Handle(QuestionsInteractiveAction request, CancellationToken cancellationToken)
        {
            var response = string.Empty;

            if (_memoryCache.TryGetValue($"_{nameof(QuestionsInteractiveActionHandler)}_{Ctx.GetTelegramChatOrDefault()?.Id}", out int questionsAsked))
            {
                if (questionsAsked >= 5)
                {
                    response = QuestionsInteractiveResponseData.OutOfMindResponses.GetRandomFromList();
                } 
                else if (questionsAsked >= 2)
                {
                    response = QuestionsInteractiveResponseData.DramaticResponses.GetRandomFromList();
                }
            }
            else
            {
                response = QuestionsInteractiveResponseData.ChillResponses.GetRandomFromList();
            }

            _memoryCache.Set($"_{nameof(QuestionsInteractiveActionHandler)}_{Ctx.GetTelegramChatOrDefault()?.Id}", questionsAsked + 1, TimeSpan.FromHours(1));
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
}