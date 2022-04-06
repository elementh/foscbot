using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Interactivity.Questions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Interactivity.Flatter;

public class FlatterInteractiveActionHandler : ActionHandler<FlatterInteractiveAction>
{
    private readonly IMemoryCache _memoryCache;
    public FlatterInteractiveActionHandler(INavigatorContext ctx, IMemoryCache memoryCache) : base(ctx)
    {
        _memoryCache = memoryCache;
    }

    public override async Task<Status> Handle(FlatterInteractiveAction action, CancellationToken cancellationToken)
    {
        var choice = RandomProvider.GetThreadRandom().Next(0, 6);
        switch (choice)
        {
            case 0:
                await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, "De nada hermozo 😘", cancellationToken: cancellationToken, replyToMessageId: action.MessageId);
                break;
            case 1:
                // Smiling rani 
                await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, "CAACAgIAAxkBAAEDJMNhdZKneWmWSMJ-5BOOyTK5y4dRpgACCgEAAjDUnRFWVFdpxm65byEE", cancellationToken: cancellationToken, replyToMessageId: action.MessageId);
                break;
            case 2:
                // Moon smiling broken 
                await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, "CAACAgIAAxkBAAEDJMlhdZQmchXArRkMCRchHWpgPNLZfgACQQoAAiqWeEhXs1wuuE0lniEE", cancellationToken: cancellationToken, replyToMessageId: action.MessageId);
                break;
            case 3:
                // Me aburris tio
                await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, "CAACAgQAAxkBAAEDJMthdZQwLAIyUcECwynw-TuPe_87fAACUgMAApjnowABWVTvcB6NosQhBA", cancellationToken: cancellationToken, replyToMessageId: action.MessageId);
                break;
            case 4:
                // P4 Arch broken
                await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, "CAACAgQAAxkBAAEDJM1hdZU5WpnzPHDOqI1SLIc5oZuz9gACWwIAApDUrQYyy_1Go-xzYiEE", cancellationToken: cancellationToken, replyToMessageId: action.MessageId);
                break;
            case 5:
                // Croco nice
                await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, "CAACAgIAAxkBAAEDJNFhdZYD0vurwr7VikMz-SbM0TDhSgACLgkAAhhC7ghmx6Iwr7yx9CEE", cancellationToken: cancellationToken, replyToMessageId: action.MessageId);
                break;
        }

        if (_memoryCache.TryGetValue($"_{nameof(QuestionsInteractiveActionHandler)}_{Ctx.GetTelegramChatOrDefault()?.Id}", out _))
        {
            _memoryCache.Remove($"_{nameof(QuestionsInteractiveActionHandler)}_{Ctx.GetTelegramChatOrDefault()?.Id}");
        }
            
        return Success();
    }
}