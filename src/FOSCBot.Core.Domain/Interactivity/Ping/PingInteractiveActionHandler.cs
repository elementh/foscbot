using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Interactivity.Ping;

public class PingInteractiveActionHandler : ActionHandler<PingInteractiveAction>
{
    public PingInteractiveActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(PingInteractiveAction request, CancellationToken cancellationToken)
    {
        var currentTime = DateTime.Now;
        var requestTime = request.MessageTimestamp;
        var delaySinceMessageWasSent = currentTime - requestTime; // ToDo Test timezone differences

        if (delaySinceMessageWasSent.TotalSeconds < 12)
        {
            await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, $"🟩 toy refinisimo bro. Delay: {delaySinceMessageWasSent.TotalSeconds}s", cancellationToken: cancellationToken, replyToMessageId: request.MessageId);
        } 
        else if (delaySinceMessageWasSent.TotalSeconds < 30)
        {
            await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, $"🟧 toy F bro. Delay: {delaySinceMessageWasSent.TotalSeconds}s", cancellationToken: cancellationToken, replyToMessageId: request.MessageId);
        }
        else
        {
            await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, $"🟥 toy joya sosio arreglame ya por dio. Delay: {delaySinceMessageWasSent.TotalSeconds}s", cancellationToken: cancellationToken, replyToMessageId: request.MessageId);
        }

        return Success();
    }
}