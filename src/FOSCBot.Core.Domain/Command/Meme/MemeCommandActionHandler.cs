using FOSCBot.Core.Domain.Command.Quote;
using FOSCBot.Core.Domain.Resources;
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
        var input = string.Empty;
        
        if (action.IsReply && !string.IsNullOrEmpty(action.Message.ReplyToMessage?.Text))
        {
            input = action.Message.ReplyToMessage.Text;
        }
        else
        {
            input = action.Message.Text?.Remove(0, action.Message.Text.IndexOf(' ') + 1);
        }

        if (input.Length > 350)
        {
            await NavigatorContext.GetTelegramClient().SendPhotoAsync(NavigatorContext.GetTelegramChat()!, CoreLinks.MuchoTexto, cancellationToken: cancellationToken);
            return Success();
        }

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