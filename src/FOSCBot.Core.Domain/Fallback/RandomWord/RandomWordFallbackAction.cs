using System;
using System.Linq;
using FOSCBot.Common.Helper;
using Microsoft.Extensions.Caching.Memory;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Fallback.RandomWord
{
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
            if (_memoryCache.TryGetValue($"_{nameof(RandomWordFallbackAction)}_{ctx.GetTelegramChat().Id}", out _))
            {
                return false;
            }

            var words = ctx.GetMessageOrDefault()?.Text.Trim().Split(" ");
            
            if (words?.Length == 1)
            {
                Word = words.FirstOrDefault() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(Word) || !Word.IsAllUpper() || Word.Length < 4)
                {
                    return false;
                }
                
                return _memoryCache.Set($"_{nameof(RandomWordFallbackAction)}_{ctx.GetTelegramChat().Id}", true, TimeSpan.FromMinutes(1));
            }

            return false;
        }
    }
}