using System.Text.RegularExpressions;
using Navigator.Abstractions;

namespace FOSCBot.Common.Helper
{
    public static class MentionHelper
    {
        private const int FoscBotUserId = 970438602;
        
        /// <summary>
        /// Returns whether the bot's was mentioned or quoted in a message.
        /// </summary>
        /// <param name="ctx">Current message's Navigator context</param>
        /// <returns>true if the bot was mentioned</returns>
        public static bool IsBotMentioned(this INavigatorContext ctx)
        {
            return ctx.Update.Message?.ReplyToMessage?.From?.Id == FoscBotUserId ||
                   (ctx.Update.Message?.Text?.ToLower().Contains("@foscbot") ?? false) ||
                   (ctx.Update.Message?.Text?.ToLower().Contains("foscbot") ?? false) ||
                   (ctx.Update.Message?.Text?.ToLower().Contains("fosbo") ?? false);
        }

        /// <summary>
        /// Returns whether the bot was requested to perform a Ping command.
        /// This will be the case when the message contains "Estas vivo" or "Estas bien".
        /// </summary>
        /// <param name="ctx">Current message's Navigator context</param>
        /// <returns>true if the bot was pinged</returns>
        public static bool IsBotPinged(this INavigatorContext ctx)
        {
            return Regex.IsMatch(ctx.Update.Message?.Text ?? string.Empty, ".*[Ee]*[Ss]*[Tt][AaÁá][Ss] [BbVv][Ii]+[EeVv]+[NnOo]+.*");
        }

        /// <summary>
        /// Returns whether the bot was sent a nice message.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns>true if the bot was flattered</returns>
        public static bool IsBotFlattered(this INavigatorContext ctx)
        {
            return ((ctx.Update.Message?.Text?.ToLower().Equals("si") ?? false) ||
                    (ctx.Update.Message?.Text?.ToLower().Equals("sí") ?? false) ||
                    (ctx.Update.Message?.Text?.ToLower().Contains("acho") ?? false) ||
                    (ctx.Update.Message?.Text?.ToLower().Contains("jajaja") ?? false) ||
                    (ctx.Update.Message?.Text?.ToLower().Contains("gracias") ?? false) ||
                    (ctx.Update.Message?.Text?.ToLower().Contains("te quiero") ?? false) ||
                    (ctx.Update.Message?.Text?.ToLower().Contains("grande") ?? false) ||
                    (ctx.Update.Message?.Text?.ToLower().Contains("increible") ?? false) ||
                    (ctx.Update.Message?.Text?.ToLower().Contains("increíble") ?? false) ||
                    (ctx.Update.Message?.Text?.ToLower().Contains("puto amo") ?? false) ||
                    Regex.IsMatch(ctx.Update.Message?.Text ?? string.Empty, @"[Jj][Oo]+[Dd][Ee]+[Rr]+"));
        }
    }
}