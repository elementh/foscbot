using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Common.Helper;
using MediatR;
using Navigator.Abstractions;
using Navigator.Abstractions.Extensions;
using Navigator.Extensions.Actions;

namespace FOSCBot.Core.Domain.Interactivity.Flatter
{
    public class FlatterMiscellaneousActionHandler : ActionHandler<FlatterMiscellaneousAction>
    {
        
        public FlatterMiscellaneousActionHandler(INavigatorContext ctx) : base(ctx)
        {
        }

        public override async Task<Unit> Handle(FlatterMiscellaneousAction request, CancellationToken cancellationToken)
        {
            var choice = RandomProvider.GetThreadRandom().Next(0, 6);
            switch (choice)
            {
                case 0:
                    await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), "De nada hermozo 😘", cancellationToken: cancellationToken, replyToMessageId: request.MessageId);
                    break;
                case 1:
                    // Smiling rani 
                    await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), "CAACAgIAAxkBAAEDJMNhdZKneWmWSMJ-5BOOyTK5y4dRpgACCgEAAjDUnRFWVFdpxm65byEE", cancellationToken: cancellationToken, replyToMessageId: request.MessageId);
                    break;
                case 2:
                    // Moon smiling broken 
                    await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), "CAACAgIAAxkBAAEDJMlhdZQmchXArRkMCRchHWpgPNLZfgACQQoAAiqWeEhXs1wuuE0lniEE", cancellationToken: cancellationToken, replyToMessageId: request.MessageId);
                    break;
                case 3:
                    // Me aburris tio
                    await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), "CAACAgQAAxkBAAEDJMthdZQwLAIyUcECwynw-TuPe_87fAACUgMAApjnowABWVTvcB6NosQhBA", cancellationToken: cancellationToken, replyToMessageId: request.MessageId);
                    break;
                case 4:
                    // P4 Arch broken
                    await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), "CAACAgQAAxkBAAEDJM1hdZU5WpnzPHDOqI1SLIc5oZuz9gACWwIAApDUrQYyy_1Go-xzYiEE", cancellationToken: cancellationToken, replyToMessageId: request.MessageId);
                    break;
                case 5:
                    // Croco nice
                    await Ctx.Client.SendStickerAsync(Ctx.GetTelegramChat(), "CAACAgIAAxkBAAEDJNFhdZYD0vurwr7VikMz-SbM0TDhSgACLgkAAhhC7ghmx6Iwr7yx9CEE", cancellationToken: cancellationToken, replyToMessageId: request.MessageId);
                    break;
            }
            
            return Unit.Value;
        }
    }
}