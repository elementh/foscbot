using Navigator.Context;
using Navigator.Extensions.Interop;
using Navigator.Extensions.Store.Bundled.Extensions;
using Navigator.Providers.Telegram;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Domain.Interop;

public class FOSCInteropActionHandler : InteropActionHandler
{
    public FOSCInteropActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    protected override Task<object> ArgsBuilder()
    {
        return Task.FromResult<object>(new
        {
            context = NavigatorContext,
            client = NavigatorContext.GetTelegramClient(),
            chat = new ChatId(NavigatorContext.GetTelegramChat().Id),
            store = NavigatorContext.GetStore()
        });
    }
}