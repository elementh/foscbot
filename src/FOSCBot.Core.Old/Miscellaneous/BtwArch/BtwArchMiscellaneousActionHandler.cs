using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Old.Miscellaneous.BtwArch;

public class BtwArchMiscellaneousActionHandler : ActionHandler<BtwArchMiscellaneousAction>
{
    public BtwArchMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(BtwArchMiscellaneousAction action, CancellationToken cancellationToken)
    {
        await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, "`Btw I run on Arch Linux.`", ParseMode.Markdown,
            replyToMessageId: action.Message.MessageId, cancellationToken: cancellationToken);

        return Success();
    }
}