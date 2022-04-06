using FOSCBot.Common.Helper;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Command.Bottomify;

public class BottomifyCommandActionHandler : ActionHandler<BottomifyCommandAction>
{
    public BottomifyCommandActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(BottomifyCommandAction request, CancellationToken cancellationToken)
    {
        string? bottomifiedText;
            
        if (request.ReplyToMessageId is not null && !string.IsNullOrWhiteSpace(Ctx.GetMessage().ReplyToMessage?.Text))
        {
            bottomifiedText = Common.Helper.Bottomify.EncodeString(Ctx.GetMessage().ReplyToMessage.Text);

            await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), bottomifiedText,
                replyToMessageId: Ctx.GetMessage().ReplyToMessage.MessageId,
                cancellationToken: cancellationToken);
                
            return Unit.Value;
        }

        var input = Ctx.GetMessage().Text.Remove(0, Ctx.GetMessage().Text.IndexOf(' ') + 1);

        if (!string.IsNullOrWhiteSpace(input) && !input.StartsWith(request.Command))
        {
            bottomifiedText = Common.Helper.Bottomify.EncodeString(input);
                
            await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), bottomifiedText,
                cancellationToken: cancellationToken);
                
            return Unit.Value;
        }

        bottomifiedText = Common.Helper.Bottomify.EncodeString(Lines[RandomProvider.GetThreadRandom().Next(0, Lines.Length)]);
            
        await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), bottomifiedText,
            cancellationToken: cancellationToken);
                
        return Unit.Value;
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