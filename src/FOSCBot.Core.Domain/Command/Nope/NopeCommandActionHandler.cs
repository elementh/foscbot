using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Command.Nope;

public class NopeCommandActionHandler : ActionHandler<NopeCommandAction>
{
    public NopeCommandActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(NopeCommandAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.Nope, cancellationToken: cancellationToken);

        return Unit.Value;
    }
}