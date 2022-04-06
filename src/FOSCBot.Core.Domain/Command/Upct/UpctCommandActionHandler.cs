using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Command.Upct;

public class UpctCommandActionHandler : ActionHandler<UpctCommandAction>
{
    public UpctCommandActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(UpctCommandAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), "CAACAgQAAxkBAAJNW16eEHOauvBkLuaD-jL95s86vn2qAAJuAwACmOejAAEys6bCdTOD7RgE", cancellationToken: cancellationToken);
        return Unit.Value;
    }
}