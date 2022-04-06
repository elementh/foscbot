using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Telegram.Bot.Types.InlineQueryResults;

namespace FOSCBot.Core.Domain.Inline.Default;

public class DefaultInlineActionHandler : ActionHandler<DefaultInlineAction>
{
    public DefaultInlineActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(DefaultInlineAction request, CancellationToken cancellationToken)
    {
        var responses = new List<InlineQueryResultArticle>();

        foreach (var (id, title, url) in Links)
        {
            responses.Add(new InlineQueryResultArticle(id, title, new InputTextMessageContent(url))
            {
                Url = url, 
                HideUrl = true,
                Description = url,
                ThumbUrl = "https://fosc.space/img/Logo-invert.webp"
            });
        }

        await Ctx.Client.AnswerInlineQueryAsync(request.InlineQueryId, responses, cancellationToken: cancellationToken);

        return Unit.Value;
    }
        
    protected static readonly IEnumerable<(string, string, string)> Links = new[]
    {
        ("mainpage", "FOSC Main Page", "https://fosc.space/"),
        ("blog", "FOSC Blog ", "https://blog.fosc.space/"),
        ("gallery", "FOSC Gallery", "https://gallery.fosc.space/"),
        ("cloud", "FOSC Cloud", "https://cloud.fosc.space/"),
        ("wiki", "FOSC Wiki", "https://doc.fosc.space/"),
        ("netdata", "FOSC Netdata stats", "https://netdata.fosc.space/"),
        ("stolencode", "For Our Stolen Code (git)", "https://git.fosc.space/"),
        ("downloads", "FOSC Downloads", "https://download.fosc.space/")
    };
}