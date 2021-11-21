using System.Text.RegularExpressions;
using Navigator.Abstractions;

namespace FOSCBot.Common.Helper
{
    public static class MentionHelper
    {
        /// <summary>
        /// Returns whether the bot's user handler was included as part of the message
        /// </summary>
        /// <param name="ctx">Current message's Navigator context</param>
        /// <returns>true if the bot was mentioned</returns>
        public static bool IsBotMentioned(this INavigatorContext ctx)
        {
            return (ctx.Update.Message?.Text?.Contains("@foscbot") ?? false) ||
                   (ctx.Update.Message?.Text?.Contains("foscbot") ?? false) ||
                   (ctx.Update.Message?.Text?.Contains("fosbo") ?? false);
        }

        /// <summary>
        /// Returns whether the bot was requested to perform a Ping command
        /// </summary>
        /// <param name="ctx">Current message's Navigator context</param>
        /// <returns>true if the bot was pinged</returns>
        public static bool IsBotPinged(this INavigatorContext ctx)
        {
            return Regex.IsMatch(ctx.Update.Message?.Text ?? string.Empty, ".*[Ee]*[Ss]*[Tt][Aa][Ss] [BbVv][Ii]+[EeVv]+[NnOo]+.*");
        }
    }
}