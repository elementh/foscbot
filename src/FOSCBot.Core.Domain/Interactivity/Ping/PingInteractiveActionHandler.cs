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

    public override async Task<Status> Handle(PingInteractiveAction action, CancellationToken cancellationToken)
    {
        var requestTime = action.Timestamp;
        var messageTime = action.Message.Date;
        var delaySinceMessageWasSent = requestTime - messageTime;

        if (delaySinceMessageWasSent.TotalSeconds < 0)
        {
            await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, $"⬛ acho puta u🅱ct arreglad ya el NTP. Delay: {delaySinceMessageWasSent.TotalSeconds}s", cancellationToken: cancellationToken, replyToMessageId: action.Message.MessageId);
        }
        else if (delaySinceMessageWasSent.TotalSeconds < 12)
        {
            await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, $"🟩 toy refinisimo bro. Delay: {delaySinceMessageWasSent.TotalSeconds}s", cancellationToken: cancellationToken, replyToMessageId: action.Message.MessageId);
        } 
        else if (delaySinceMessageWasSent.TotalSeconds < 30)
        {
            await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, $"🟧 toy F bro. Delay: {delaySinceMessageWasSent.TotalSeconds}s", cancellationToken: cancellationToken, replyToMessageId: action.Message.MessageId);
        }
        else
        {
            await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, $"🟥 toy joya sosio arreglame ya por dio. Delay: {delaySinceMessageWasSent.TotalSeconds}s", cancellationToken: cancellationToken, replyToMessageId: action.Message.MessageId);
        }

        return Success();
    }
}