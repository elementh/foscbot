using FOSCBot.Infrastructure.Contract.Service;
using MediatR;
using Navigator.Actions;
using Navigator.Context;

namespace FOSCBot.Core.Domain.Command.Quote;

public class QuoteCommandActionHandler : ActionHandler<QuoteCommandAction>
{
    private readonly IInspiroService _inspiroService;

    public QuoteCommandActionHandler(INavigatorContext ctx, IInspiroService inspiroService) : base(ctx)
    {
        _inspiroService = inspiroService;
    }

    public override async Task<Unit> Handle(QuoteCommandAction request, CancellationToken cancellationToken)
    {
        var image = await _inspiroService.GetInspiroImage(cancellationToken);

        await Ctx.Client.SendPhotoAsync(Ctx.GetTelegramChat(), image, cancellationToken: cancellationToken);
            
        return Unit.Value;
    }
}