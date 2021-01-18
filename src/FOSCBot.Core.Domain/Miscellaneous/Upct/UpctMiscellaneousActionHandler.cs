using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Common.Helper;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

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

            if (RandomProvider.GetThreadRandom().NextDouble() > 0.8d)
                await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), "cAmPuS dE eXcElEnCiA iNtErNaCiOnAl", cancellationToken: cancellationToken);

            return Unit.Value;
        }
    }
}
