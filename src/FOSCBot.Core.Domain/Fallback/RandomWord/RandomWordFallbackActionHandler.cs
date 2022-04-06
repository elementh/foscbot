using FOSCBot.Infrastructure.Contract.Service;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;

namespace FOSCBot.Core.Domain.Fallback.RandomWord;

public class RandomWordFallbackActionHandler : ActionHandler<RandomWordFallbackAction>
{
    private readonly IGiphyService _giphyService;
    public RandomWordFallbackActionHandler(INavigatorContext ctx, IGiphyService giphyService) : base(ctx)
    {
        _giphyService = giphyService;
    }

    public override async Task<Status> Handle(RandomWordFallbackAction request, CancellationToken cancellationToken)
    {
        var gifUrl = await _giphyService.Get(request.Word, cancellationToken);

        if (gifUrl is not null)
        {
            await NavigatorContext.GetTelegramClient().SendAnimationAsync(NavigatorContext.GetTelegramChat()!, new InputOnlineFile(gifUrl), cancellationToken: cancellationToken);
        }
            
        return Success();
    }
}