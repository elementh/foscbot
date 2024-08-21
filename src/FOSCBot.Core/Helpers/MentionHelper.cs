using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Helpers;

public static class MentionHelper
{
    /// <summary>
    /// The current instance's of FOSCBot ID
    /// </summary>
    private const int FoscBotUserId = 970438602;

    public static bool IsBotQuotedOrMentioned(this Update update)
    {
        return update.IsBotQuoted() || update.IsBotMentioned();
    }

    public static bool IsBotMentioned(this Update update)
    {
        return update.Message?.Text switch
        {
            { } text when text.Contains("@foscbot") => true,
            { } text when text.Contains("foscbot") => true,
            { } text when text.Contains("fosbo") => true,
            _ => false
        };
    }

    public static bool IsBotQuoted(this Update update)
    {
        return update.Message?.ReplyToMessage?.From?.Id == FoscBotUserId;
    }

    public static bool IsBotPinged(this Update update)
    {
        return Regex.IsMatch(update.Message?.Text ?? string.Empty, ".*[Ee]*[Ss]*[Tt][AaÁá][Ss] [BbVv][Ii]+[EeVv]+[NnOo]+.*");
    }

    public static bool IsBotFlattered(this Update update)
    {
        return ((update.Message?.Text?.ToLower().Equals("si") ?? false) ||
                (update.Message?.Text?.ToLower().Equals("sí") ?? false) ||
                (update.Message?.Text?.ToLower().Contains("acho") ?? false) ||
                (update.Message?.Text?.ToLower().Contains("jajaja") ?? false) ||
                (update.Message?.Text?.ToLower().Contains("gracias") ?? false) ||
                (update.Message?.Text?.ToLower().Contains("te quiero") ?? false) ||
                (update.Message?.Text?.ToLower().Contains("grande") ?? false) ||
                (update.Message?.Text?.ToLower().Contains("increible") ?? false) ||
                (update.Message?.Text?.ToLower().Contains("increíble") ?? false) ||
                (update.Message?.Text?.ToLower().Contains("puto amo") ?? false) ||
                Regex.IsMatch(update.Message?.Text ?? string.Empty, @"[Jj][Oo]+[Dd][Ee]+[Rr]+"));
    }

    public static bool IsBotBeingToldBadThings(this Update update)
    {
        return update.Message?.Text switch
        {
            { } text when text.Contains("bad bot", StringComparison.CurrentCultureIgnoreCase) => true,
            { } text when text.Contains("bot malo", StringComparison.CurrentCultureIgnoreCase) => true,
            { } text when text.Contains("mal bot", StringComparison.CurrentCultureIgnoreCase) => true,
            { } text when text.Contains("basta", StringComparison.CurrentCultureIgnoreCase) => true,
            { } text when text.Contains("para", StringComparison.CurrentCultureIgnoreCase) => true,
            { } text when text.Contains("capullo", StringComparison.CurrentCultureIgnoreCase) => true,
            _ => false
        };
    }
}