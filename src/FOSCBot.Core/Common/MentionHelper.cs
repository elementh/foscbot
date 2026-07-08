using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Common;

public static class MentionHelper
{
    /// <summary>
    ///     The current instance's of FOSCBot ID
    /// </summary>
    private const int FoscBotUserId = 970438602;

    public static bool IsBotQuotedOrMentioned(this Update update)
    {
        return update.IsBotQuoted() || update.IsBotMentioned();
    }

    public static bool IsBotMentioned(this Update update)
    {
        // Case-insensitive: mobile keyboards capitalize "Foscbot"/"Fosbo" at message start.
        return update.Message?.Text is { } text &&
               (text.Contains("foscbot", StringComparison.OrdinalIgnoreCase) ||
                text.Contains("fosbo", StringComparison.OrdinalIgnoreCase));
    }

    public static bool IsBotQuoted(this Update update)
    {
        return update.Message?.ReplyToMessage?.From?.Id == FoscBotUserId && update.Message?.From?.Id != FoscBotUserId;
    }

    public static bool IsBotPinged(this Update update)
    {
        return Regex.IsMatch(update.Message?.Text ?? string.Empty, @"\b(?:es)?t[aá]s\s+[bv](?:i+e+n+|i+v+o+)\b",
            RegexOptions.IgnoreCase);
    }

    public static bool IsBotFlattered(this Update update)
    {
        if (update.Message?.Text is not { } text) return false;

        var lowered = text.ToLower();

        // acho/dios need word boundaries (macho, borracho, adios, medios...), and
        // grande needs praise context or it matches any big object being described.
        return lowered.Equals("si") ||
               lowered.Equals("sí") ||
               lowered.Contains("jajaja") ||
               lowered.Contains("gracias") ||
               lowered.Contains("thanks") ||
               lowered.Contains("thx") ||
               lowered.Contains("te quiero") ||
               lowered.Contains("increible") ||
               lowered.Contains("increíble") ||
               lowered.Contains("puto amo") ||
               Regex.IsMatch(text, @"\bacho+\b", RegexOptions.IgnoreCase) ||
               Regex.IsMatch(text, @"\bdios\b", RegexOptions.IgnoreCase) ||
               Regex.IsMatch(text, @"\b(?:eres\s+(?:el\s+)?(?:m[aá]s|muy)\s+grande|qu[eé]\s+grande|grande,?\s+fos[cb]?bot)\b", RegexOptions.IgnoreCase) ||
               Regex.IsMatch(text, @"\bjo+de+r+\b", RegexOptions.IgnoreCase);
    }

    public static bool IsBotBeingToldBadThings(this Update update)
    {
        return update.Message?.Text switch
        {
            { } text when text.Contains("bad bot", StringComparison.CurrentCultureIgnoreCase) => true,
            { } text when text.Contains("bot malo", StringComparison.CurrentCultureIgnoreCase) => true,
            { } text when Regex.IsMatch(text, @"\bmal bot\b", RegexOptions.IgnoreCase) => true,
            { } text when Regex.IsMatch(text, @"\bbasta\b", RegexOptions.IgnoreCase) => true,
            // "para" only as a standalone stop command (optionally addressed to the bot);
            // as a bare Contains it matched the most common Spanish preposition.
            { } text when Regex.IsMatch(text, @"^\s*(?:@?fos[cb]?bot[\s,:!]+)?¡*(?:para|párate?|parate)(?:\s+ya)?[\s!.]*(?:@?fos[cb]?bot)?[\s!.]*$", RegexOptions.IgnoreCase) => true,
            { } text when text.Contains("capullo", StringComparison.CurrentCultureIgnoreCase) => true,
            _ => false
        };
    }
}