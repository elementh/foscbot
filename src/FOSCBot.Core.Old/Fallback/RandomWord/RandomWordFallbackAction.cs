using FOSCBot.Common.Helper;
using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Extensions.Cooldown;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Old.Fallback.RandomWord;

[Cooldown(Seconds = 15 * 60)]
[ActionPriority(Navigator.Actions.Priority.Low - 100)]
public class RandomWordFallbackAction : MessageAction
{
    public string Word { get; protected set; } = string.Empty;
 
    public RandomWordFallbackAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        
    }

    public override bool CanHandleCurrentContext()
    {
        if (RandomProvider.GetThreadRandom().NextDouble() > 0.6)
        {
            return false;
        }

        Word = Message.Text?.Trim().Split(" ").FirstOrDefault() ?? string.Empty;

        return !string.IsNullOrWhiteSpace(Word) && Word.IsAllUpper() && Word.Length >= 4 && !Word.Contains("XDDD");
    }
}