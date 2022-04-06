using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Command.Coronashark;

public class CoronasharkCommandActionHandler : ActionHandler<CoronasharkCommandAction>
{
    public CoronasharkCommandActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(CoronasharkCommandAction action, CancellationToken cancellationToken)
    {
        await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, CoronasharkSticker, cancellationToken: cancellationToken);
            
        return Success();
    }
        
    public static string CoronasharkSticker = "CAACAgQAAxkBAAI4_l59L095Ep-xxos5f_7KBYkVlbu5AAKcBgACL9trAAF-dsaP9FZw_hgE";
}