using FOSCBot.Common.Helper;
using FOSCBot.Infrastructure.Contract.Service;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace FOSCBot.Core.Domain.Fallback.Default;

public class DefaultFallbackActionHandler : ActionHandler<DefaultFallbackAction>
{
    protected ILipsumService LipsumService;

    public DefaultFallbackActionHandler(INavigatorContext ctx, ILipsumService lipsumService) : base(ctx)
    {
        LipsumService = lipsumService;
    }

    public override async Task<Status> Handle(DefaultFallbackAction request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(Ctx.GetMessageOrDefault()?.Text) && Bottomify.IsEncoded(Ctx.GetMessage().Text))
        {
            await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!,
                $"`Fellow humans I have decoded these words of wisdom:` \n_{Bottomify.DecodeString(Ctx.GetMessage().Text)}_",
                ParseMode.Markdown,
                cancellationToken: cancellationToken);
        }
            
        if (RandomProvider.GetThreadRandom().Next(0, 600) < 598)
        {
            return Success();
        }
            
        var sentence = string.Empty;

        var odds = RandomProvider.GetThreadRandom().Next(0, 20);

        if (odds >= 0 && odds < 5)
        {
            sentence = await LipsumService.GetBacon(cancellationToken: cancellationToken);
        }
        else if (odds >= 5 && odds < 10)
        {
            sentence = await LipsumService.GetMetaphorSentence(cancellationToken: cancellationToken);
        }
        else if (Ctx.GetMessageOrDefault()?.Text.Split(' ').Length > 3)
        {
            sentence = MockFilter.Apply(Ctx.GetMessage().Text);
        }

        if (!string.IsNullOrWhiteSpace(sentence))
        {
            await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, sentence, ParseMode.Markdown,
                replyToMessageId: request.MessageId, cancellationToken: cancellationToken);
        }

        return Success();
    }
}