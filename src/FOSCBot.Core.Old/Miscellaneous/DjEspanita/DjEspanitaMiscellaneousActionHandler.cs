using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Old.Miscellaneous.DjEspanita;

public class DjEspanitaMiscellaneousActionHandler : ActionHandler<DjEspanitaMiscellaneousAction>
{
    public DjEspanitaMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(DjEspanitaMiscellaneousAction action, CancellationToken cancellationToken)
    {
        await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, DjSticker, cancellationToken: cancellationToken);
            
        return Success();
    }
        
    public static string DjSticker = "CAACAgQAAxkBAAJWPF6i8ixK0-SqAayKyCdmHYcYFix3AAIhAAN87RspJn8XTAs-3tUZBA";
}