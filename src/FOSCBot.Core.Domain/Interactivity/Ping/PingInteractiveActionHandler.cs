using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Interactivity.Ping
{
    public class PingInteractiveActionHandler : ActionHandler<PingInteractiveAction>
    {
        public PingInteractiveActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(PingInteractiveAction request, CancellationToken cancellationToken)
        {
            var currentTime = new DateTime();
            var requestTime = request.MessageTimestamp;
            var delaySinceMessageWasSent = currentTime - requestTime; // ToDo Test timezone differences

            if (delaySinceMessageWasSent.TotalMinutes < 1)
            {
                await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), $"🟩 toy refinisimo bro. Delay: {delaySinceMessageWasSent.TotalSeconds}s", cancellationToken: cancellationToken, replyToMessageId: request.MessageId);
            } 
            else if (delaySinceMessageWasSent.TotalMinutes < 3)
            {
                await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), $"🟧 toy F bro. Delay: {delaySinceMessageWasSent.TotalSeconds}s", cancellationToken: cancellationToken, replyToMessageId: request.MessageId);
            }
            else
            {
                await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), $"🟥 toy joya sosio arreglame ya por dio. Delay: {delaySinceMessageWasSent.TotalSeconds}s", cancellationToken: cancellationToken, replyToMessageId: request.MessageId);
            }

            return Unit.Value;
        }
    }
}