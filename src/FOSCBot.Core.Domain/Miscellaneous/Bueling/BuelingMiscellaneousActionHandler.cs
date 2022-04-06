using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Miscellaneous.Bueling;

public class BuelingMiscellaneousActionHandler : ActionHandler<BuelingMiscellaneousAction>
{
    public BuelingMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(BuelingMiscellaneousAction action, CancellationToken cancellationToken)
    {
        await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, "Did some carbon based life form just mention...", cancellationToken: cancellationToken);
        await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, "CAACAgQAAxkBAAJJpl6bSONlqhE0C21-0T9V9YHxfqPKAAKZBgACL9trAAHwqRcYUmB_gRgE", cancellationToken: cancellationToken);
            
        return Success();
    }
}