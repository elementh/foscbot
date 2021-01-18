using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Common.Helper;
using FOSCBot.Infrastructure.Contract.Service;
using MediatR;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Domain.Fallback.Default
{
    public class DefaultFallbackActionHandler : ActionHandler<DefaultFallbackAction>
    {
        protected ILipsumService LipsumService;

        public DefaultFallbackActionHandler(INavigatorContext ctx, ILipsumService lipsumService) : base(ctx)
        {
            LipsumService = lipsumService;
        }

        public override async Task<Unit> Handle(DefaultFallbackAction request, CancellationToken cancellationToken)
        {
            var sentence = string.Empty;

            var odds = RandomProvider.GetThreadRandom().Next(0, 15);

            if (odds >= 0 && odds < 5)
            {
                sentence = await LipsumService.GetBacon(cancellationToken: cancellationToken);
            }
            else if (odds >= 5 && odds < 10)
            {
                sentence = await LipsumService.GetMetaphorSentence(cancellationToken: cancellationToken);
            }
            else if (Ctx.GetMessageOrDefault()?.Text.Split(' ').Length > 3)
            {
                sentence = MockFilter.Apply(Ctx.GetMessage().Text);
            }

            if (!string.IsNullOrWhiteSpace(sentence))
            {
                await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), sentence, ParseMode.Markdown,
                    replyToMessageId: request.MessageId, cancellationToken: cancellationToken);
            }

            return Unit.Value;
        }
    }
}