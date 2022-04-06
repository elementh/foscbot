using Microsoft.Extensions.Caching.Memory;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Dineros;

public class DinerosMiscellaneousAction : MessageAction
{
    private readonly IMemoryCache _memoryCache;

    public string Word { get; protected set; }

    public DinerosMiscellaneousAction(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public override bool CanHandleCurrentContext()
    {
        if (_memoryCache.TryGetValue($"_{nameof(DinerosMiscellaneousAction)}_{ctx.GetTelegramChatOrDefault()?.Id}", out _))
        {
            return false;
        }

        if ((Message.Text?.ToLower().Contains("pobres") ?? false) 
            || (Message.Text?.ToLower().Contains("tesla") ?? false)
            || (Message.Text?.ToLower().Contains("dineros") ?? false))
        {
            try
            {
                _memoryCache.Set($"_{nameof(DinerosMiscellaneousAction)}_{ctx.GetTelegramChatOrDefault()?.Id}", 1,
                    TimeSpan.FromMinutes(5));

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