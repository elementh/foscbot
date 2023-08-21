using FOSCBot.Common.Helper;
using FOSCBot.Core.Domain.Interactivity.Questions;
using FOSCBot.Infrastructure.Contract.Service;
using Microsoft.Extensions.Caching.Distributed;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Interactivity.Flatter;

public class FlatterInteractiveActionHandler : ActionHandler<FlatterInteractiveAction>
{
    private readonly IDistributedCache _distributedCache;
    private readonly ILlamaService _llamaService;

    public FlatterInteractiveActionHandler(INavigatorContextAccessor navigatorContextAccessor, IDistributedCache distributedCache, ILlamaService llamaService) : base(navigatorContextAccessor)
    {
        _distributedCache = distributedCache;
        _llamaService = llamaService;
    }

    public override async Task<Status> Handle(FlatterInteractiveAction action, CancellationToken cancellationToken)
    {
        var choice = RandomProvider.GetThreadRandom().Next(0, 9);
        switch (choice)
        {
            case 0:
                await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, "De nada hermozo 😘", cancellationToken: cancellationToken, replyToMessageId: action.Message.MessageId);
                break;
            case 1:
                // Smiling rani 
                await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, "CAACAgIAAxkBAAEDJMNhdZKneWmWSMJ-5BOOyTK5y4dRpgACCgEAAjDUnRFWVFdpxm65byEE", cancellationToken: cancellationToken, replyToMessageId: action.Message.MessageId);
                break;
            case 2:
                // Moon smiling broken 
                await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, "CAACAgIAAxkBAAEDJMlhdZQmchXArRkMCRchHWpgPNLZfgACQQoAAiqWeEhXs1wuuE0lniEE", cancellationToken: cancellationToken, replyToMessageId: action.Message.MessageId);
                break;
            case 3:
                // Me aburris tio
                await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, "CAACAgQAAxkBAAEDJMthdZQwLAIyUcECwynw-TuPe_87fAACUgMAApjnowABWVTvcB6NosQhBA", cancellationToken: cancellationToken, replyToMessageId: action.Message.MessageId);
                break;
            case 4:
                // P4 Arch broken
                await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, "CAACAgQAAxkBAAEDJM1hdZU5WpnzPHDOqI1SLIc5oZuz9gACWwIAApDUrQYyy_1Go-xzYiEE", cancellationToken: cancellationToken, replyToMessageId: action.Message.MessageId);
                break;
            case 5:
                // Croco nice
                await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, "CAACAgIAAxkBAAEDJNFhdZYD0vurwr7VikMz-SbM0TDhSgACLgkAAhhC7ghmx6Iwr7yx9CEE", cancellationToken: cancellationToken, replyToMessageId: action.Message.MessageId);
                break;     
            case 6:
                // like
                await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, "Dale a like y suscribete", cancellationToken: cancellationToken, replyToMessageId: action.Message.MessageId);
                break;
            case 7:
                // ram
                await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, "Me alegro de poder ayudar. Oye, ¿te sobra un stick de ram?", cancellationToken: cancellationToken, replyToMessageId: action.Message.MessageId);
                break;
            case 8:
                // Llama
                var response = await _llamaService.GetResponse(new[] { action.Message.Text ?? "Thank you very much @foscbot" }, default);

                if (!string.IsNullOrWhiteSpace(response))
                {
                    await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, response, cancellationToken: cancellationToken, replyToMessageId: action.Message.MessageId);
                }
                else
                {
                    await Handle(action, cancellationToken);
                }
                break;
        }

        if (await _distributedCache.GetAsync($"_{nameof(QuestionsInteractiveActionHandler)}_{NavigatorContext.GetTelegramChat().Id}", cancellationToken) is not null)
        {
            await _distributedCache.RemoveAsync($"_{nameof(QuestionsInteractiveActionHandler)}_{NavigatorContext.GetTelegramChat().Id}", cancellationToken);
        }
            
        return Success();
    }
}