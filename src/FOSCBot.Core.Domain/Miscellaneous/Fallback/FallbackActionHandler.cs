using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Common.Helper;
using FOSCBot.Infrastructure.Contract.Service;
using MediatR;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Domain.Miscellaneous.Fallback
{
    public class FallbackActionHandler : ActionHandler<FallbackAction>
    {
        protected ILipsumService LipsumService;
        
        public FallbackActionHandler(INavigatorContext ctx, ILipsumService lipsumService) : base(ctx)
        {
            LipsumService = lipsumService;
        }

        public override async Task<Unit> Handle(FallbackAction request, CancellationToken cancellationToken)
        {
            string sentence;
            
            if (RandomProvider.GetThreadRandom().Next(0, 10) > 5)
            {
                sentence = await LipsumService.GetBacon(cancellationToken: cancellationToken);
            }
            else
            {
                sentence = await LipsumService.GetMetaphorSentence(cancellationToken: cancellationToken);
            }
            
            await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), sentence, ParseMode.Markdown,
                replyToMessageId: request.MessageId, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}