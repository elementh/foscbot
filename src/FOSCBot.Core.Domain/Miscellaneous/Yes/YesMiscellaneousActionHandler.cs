using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Miscellaneous.Yes;

public class YesMiscellaneousActionHandler : ActionHandler<YesMiscellaneousAction>
{
    public YesMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(YesMiscellaneousAction request, CancellationToken cancellationToken)
    {
        await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, Yes, cancellationToken: cancellationToken);

        return Success();
    }
        
    public static readonly string Yes = "CAACAgQAAxkBAAI5HF59wcwDyRdwkEU3m_4CMMoz06CwAAKvAwACSy1sAAHbWFZ7iah6TRgE";
}