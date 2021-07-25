using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Source
{
    public class SourceMiscellaneousActionHandler : ActionHandler<SourceMiscellaneousAction>
    {
        public SourceMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(SourceMiscellaneousAction request, CancellationToken cancellationToken)
        {
            await Ctx.Client.SendPhotoAsync(Ctx.GetTelegramChat(), CoreLinks.Source, cancellationToken: cancellationToken);

            return Unit.Value;
        }
    }
}