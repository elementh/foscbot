using System;
using System.Linq;
using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Fallback.RandomWord;
using Microsoft.Extensions.Caching.Memory;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Miscellaneous.Dineros
{
    public class DinerosMiscellaneousAction : MessageAction
    {
        private readonly IMemoryCache _memoryCache;

        public string Word { get; protected set; }

        public DinerosMiscellaneousAction(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public override bool CanHandle(INavigatorContext ctx)
        {
            if (_memoryCache.TryGetValue($"_{nameof(DinerosMiscellaneousAction)}_{ctx.GetTelegramChatOrDefault()?.Id}", out _))
            {
                return false;
            }

            if ((ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("pobres") ?? false) 
                || (ctx.GetMessageOrDefault()?.Text?.ToLower().Contains("tesla") ?? false))
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
}