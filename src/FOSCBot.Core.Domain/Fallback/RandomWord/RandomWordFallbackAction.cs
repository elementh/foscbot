using FOSCBot.Common.Helper;
using Microsoft.Extensions.Caching.Memory;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Fallback.RandomWord;

public class RandomWordFallbackAction : MessageAction
{
    public new int Order;

    private readonly IMemoryCache _memoryCache;

    public string Word { get; protected set; }

    public RandomWordFallbackAction(IMemoryCache memoryCache)
    {
        Order = 1050;
        _memoryCache = memoryCache;
    }

    public override bool CanHandle(INavigatorContext ctx)
    {
        if (_memoryCache.TryGetValue($"_{nameof(RandomWordFallbackAction)}_{ctx.GetTelegramChatOrDefault()?.Id}", out _))
        {
            return false;
        }

        if (RandomProvider.GetThreadRandom().NextDouble() > 0.6)
        {
            return false;
        }

        var words = ctx.GetMessageOrDefault()?.Text?.Trim().Split(" ");

        if (words?.Length == 1)
        {
            Word = words.FirstOrDefault() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(Word) || !Word.IsAllUpper() || Word.Length < 4)
            {
                return false;
            }
                
            if (Word.Contains("XDDD"))
            {
                return false;
            }

            try
            {
                _memoryCache.Set($"_{nameof(RandomWordFallbackAction)}_{ctx.GetTelegramChatOrDefault()?.Id}", 1, TimeSpan.FromMinutes(15));

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