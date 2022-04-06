using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Resources;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Miscellaneous.Cagaste;

public class CagasteMiscellaneousActionHandler : ActionHandler<CagasteMiscellaneousAction>
{
    public CagasteMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(CagasteMiscellaneousAction action, CancellationToken cancellationToken)
    {
        if (RandomProvider.GetThreadRandom().NextDouble() <= 0.5d)
            await NavigatorContext.GetTelegramClient().SendPhotoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.CagasteGoku, cancellationToken: cancellationToken);
        else
            await NavigatorContext.GetTelegramClient().SendPhotoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.CagasteShark, cancellationToken: cancellationToken);

        return Success();
    }
}