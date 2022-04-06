using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.DjEspanita;

public class DjEspanitaMiscellaneousActionHandler : ActionHandler<DjEspanitaMiscellaneousAction>
{
    public DjEspanitaMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(DjEspanitaMiscellaneousAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), DjSticker, cancellationToken: cancellationToken);
            
        return Unit.Value;
    }
        
    public static string DjSticker = "CAACAgQAAxkBAAJWPF6i8ixK0-SqAayKyCdmHYcYFix3AAIhAAN87RspJn8XTAs-3tUZBA";
}