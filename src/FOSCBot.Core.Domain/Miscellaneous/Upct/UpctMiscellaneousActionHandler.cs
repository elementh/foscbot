using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Upct
{
    public class UpctMiscellaneousActionHandler : ActionHandler<UpctMiscellaneousAction>
    {
        public UpctMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(UpctMiscellaneousAction request, CancellationToken cancellationToken)
        {
            var stickerString = RandomProvider.GetThreadRandom().NextDouble() > 0.2d
                ? "CAACAgQAAxkBAAJNW16eEHOauvBkLuaD-jL95s86vn2qAAJuAwACmOejAAEys6bCdTOD7RgE"
                : "CAACAgQAAxkBAAJNXV6eEJLQHwl-8el7YOYYJUF9l8ymAAJZAgACkNStBjfoiv3ywvd8GAQ";
            await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), stickerString, cancellationToken: cancellationToken);

            if (RandomProvider.GetThreadRandom().NextDouble() > 0.1d)
                await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), "cAmPuS dE eXcElEnCiA iNtErNaCiOnAl", cancellationToken: cancellationToken);

            return Unit.Value;
        }
    }
}
