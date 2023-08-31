using FOSCBot.Core.Domain.Command.Quote;
using FOSCBot.Infrastructure.Contract.Service;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Domain.Command.Meme;

public class MemeCommandActionHandler : ActionHandler<MemeCommandAction>
{
    private readonly IMemeService _memeService;

    public MemeCommandActionHandler(INavigatorContextAccessor navigatorContextAccessor, IMemeService memeService) : base(navigatorContextAccessor)
    {
        _memeService = memeService;
    }

    public override async Task<Status> Handle(MemeCommandAction action, CancellationToken cancellationToken)
    {
        Stream? image = default;
        
        if (action.IsReply && !string.IsNullOrEmpty(action.Message.ReplyToMessage?.Text))
        {
            image = await _memeService.GenerateMeme(action.Message.ReplyToMessage.Text, cancellationToken);
        }

        var input = action.Message.Text?.Remove(0, action.Message.Text.IndexOf(' ') + 1);

        if (!string.IsNullOrWhiteSpace(input) && !input.StartsWith(action.Command))
        {
            image = await _memeService.GenerateMeme(input, cancellationToken);
        }

        if (image is not null)
        {
            if (action.IsReply)
            {
                await NavigatorContext.GetTelegramClient().SendPhotoAsync(
                    chatId: NavigatorContext.GetTelegramChat()!, 
                    photo: new InputMedia(image, Guid.NewGuid().ToString()), 
                    replyToMessageId: action.Message.ReplyToMessage?.MessageId,
                    cancellationToken: cancellationToken);
            }
            else
            {
                await NavigatorContext.GetTelegramClient().SendPhotoAsync(
                    chatId: NavigatorContext.GetTelegramChat()!, 
                    photo: new InputMedia(image, Guid.NewGuid().ToString()),
                    cancellationToken: cancellationToken);
            }
        }
        else
        {
            await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, "no meme for you", cancellationToken: cancellationToken);
        }
        
        return Success();
    }
}