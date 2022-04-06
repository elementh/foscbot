using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Command.Upct;

public class UpctCommandActionHandler : ActionHandler<UpctCommandAction>
{
    public UpctCommandActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(UpctCommandAction request, CancellationToken cancellationToken)
    {
        await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, "CAACAgQAAxkBAAJNW16eEHOauvBkLuaD-jL95s86vn2qAAJuAwACmOejAAEys6bCdTOD7RgE", cancellationToken: cancellationToken);
        return Success();
    }
}