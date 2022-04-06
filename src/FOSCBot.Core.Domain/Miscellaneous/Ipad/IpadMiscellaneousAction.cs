using Microsoft.Extensions.Caching.Memory;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Ipad;

public class IpadMiscellaneousAction : MessageAction
{
    private readonly IMemoryCache _memoryCache;

    public IpadMiscellaneousAction(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public override bool CanHandle(INavigatorContext ctx)
    {
        if (_memoryCache.TryGetValue($"_{nameof(IpadMiscellaneousAction)}_{ctx.GetTelegramChatOrDefault()?.Id}", out _))
        {
            return false;
        }

        if (ctx.Update.Message.Text?.ToLower().Contains(" ipad") ?? false)
        {
            try
            {
                _memoryCache.Set($"_{nameof(IpadMiscellaneousAction)}_{ctx.GetTelegramChatOrDefault()?.Id}", 1, TimeSpan.FromMinutes(15));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        return false;
    }
}