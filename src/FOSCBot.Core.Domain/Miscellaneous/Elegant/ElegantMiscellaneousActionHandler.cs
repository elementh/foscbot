using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Core.Domain.Resources;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Elegant
{
    public class ElegantMiscellaneousActionHandler : ActionHandler<ElegantMiscellaneousAction>
    {
        private const string Quote = "Elegance is achieved when all that is superfluous has been discarded and the human being discovers simplicity and concentration: " +
                                     "the simpler and more sober the posture, the more beautiful it will be.";
        
        public ElegantMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(ElegantMiscellaneousAction request, CancellationToken cancellationToken)
        {
            await Ctx.Client.SendVideoAsync(Ctx.GetTelegramChat(), CoreLinks.Elegant, caption: Quote, cancellationToken: cancellationToken);
            return Unit.Value;
        }
    }
}