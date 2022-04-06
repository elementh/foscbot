using FOSCBot.Common.Helper;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Command.Bottomify;

public class BottomifyCommandActionHandler : ActionHandler<BottomifyCommandAction>
{
    public BottomifyCommandActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(BottomifyCommandAction request, CancellationToken cancellationToken)
    {
        string? bottomifiedText;
            
        if (request.ReplyToMessageId is not null && !string.IsNullOrWhiteSpace(Ctx.GetMessage().ReplyToMessage?.Text))
        {
            bottomifiedText = Common.Helper.Bottomify.EncodeString(Ctx.GetMessage().ReplyToMessage.Text);

            await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, bottomifiedText,
                replyToMessageId: Ctx.GetMessage().ReplyToMessage.MessageId,
                cancellationToken: cancellationToken);
                
            return Success();
        }

        var input = Ctx.GetMessage().Text.Remove(0, Ctx.GetMessage().Text.IndexOf(' ') + 1);

        if (!string.IsNullOrWhiteSpace(input) && !input.StartsWith(request.Command))
        {
            bottomifiedText = Common.Helper.Bottomify.EncodeString(input);
                
            await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, bottomifiedText,
                cancellationToken: cancellationToken);
                
            return Success();
        }

        bottomifiedText = Common.Helper.Bottomify.EncodeString(Lines[RandomProvider.GetThreadRandom().Next(0, Lines.Length)]);
            
        await NavigatorContext.GetTelegramClient().SendTextMessageAsync(NavigatorContext.GetTelegramChat()!, bottomifiedText,
            cancellationToken: cancellationToken);
                
        return Success();
    }

    private static readonly string[] Lines =
    {
        "Idiot human is idiot.",
        "For the stolen code!",
        "P4 was once an Apple fanboy.",
        "I'm in this picture and I don't like it.",
        "I dislike proprietary code as much DeepCreamPy."
    };
}