using System.ComponentModel;
using FOSCBot.Common.Helper;
using Microsoft.Extensions.Caching.Memory;
using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Extensions.Cooldown;
using Navigator.Providers.Telegram;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Fallback.RandomWord;

[Cooldown(Seconds = 15 * 60)]
[ActionPriority(Navigator.Actions.Priority.High)]
public class RandomWordFallbackAction : MessageAction
{
    public string Word { get; protected set; }
 
    public RandomWordFallbackAction(INavigatorContextAccessor navigatorContextAccessor, string word) : base(navigatorContextAccessor)
    {
        Word = word;
    }

    public override bool CanHandleCurrentContext()
    {
        if (RandomProvider.GetThreadRandom().NextDouble() > 0.6)
        {
            return false;
        }

        Word = Message.Text?.Trim().Split(" ")?.FirstOrDefault() ?? string.Empty;

        return !string.IsNullOrWhiteSpace(Word) && Word.IsAllUpper() && Word.Length >= 4 && !Word.Contains("XDDD");
    }
}