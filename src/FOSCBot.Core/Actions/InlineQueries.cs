using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Navigator.Client;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;

namespace FOSCBot.Core.Actions;

public static class InlineQueries
{
    public static void RegisterInlineQueries(this BotActionCatalogFactory catalog)
    {
        catalog.OnInlineQuery((InlineQuery _) => true, async (INavigatorClient client, InlineQuery query) =>
        {
            IEnumerable<(string, string, string)> links =
            [
                ("mainpage", "FOSC Main Page", "https://fosc.space/"),
                ("blog", "FOSC Blog ", "https://blog.fosc.space/"),
                ("gallery", "FOSC Gallery", "https://gallery.fosc.space/"),
                ("cloud", "FOSC Cloud", "https://cloud.fosc.space/"),
                ("wiki", "FOSC Wiki", "https://doc.fosc.space/"),
                ("netdata", "FOSC Netdata stats", "https://netdata.fosc.space/"),
                ("stolencode", "For Our Stolen Code (git)", "https://git.fosc.space/"),
                ("downloads", "FOSC Downloads", "https://download.fosc.space/")
            ];
            
            var responses = new List<InlineQueryResultArticle>();

            foreach (var (id, title, url) in links)
            {
                responses.Add(new InlineQueryResultArticle(id, title, new InputTextMessageContent(url))
                {
                    Url = url, 
                    HideUrl = true,
                    Description = url,
                    ThumbnailUrl = "https://fosc.space/img/Logo-invert.webp"
                });
            }
            
            await client.AnswerInlineQueryAsync(query.Id, responses);
        });
    }
}