using FOSCBot.Common.Helper;
using MediatR;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Miscellaneous.Jeje;

public class JejeMiscellaneousActionHandler : ActionHandler<JejeMiscellaneousAction>
{
    public JejeMiscellaneousActionHandler(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

    public override async Task<Status> Handle(JejeMiscellaneousAction request, CancellationToken cancellationToken)
    {
        var randomSticker = Stickers[RandomProvider.GetThreadRandom().Next(0, Stickers.Length)];

        await NavigatorContext.GetTelegramClient().SendStickerAsync(NavigatorContext.GetTelegramChat()!, randomSticker, cancellationToken: cancellationToken);
            
        return Success();
    }
        
    public static readonly string[] Stickers = {
        "CAACAgIAAxkBAAI5Dl59vPOH6MA6Uzua49AHRz-q5mMUAAIKAQACMNSdEVZUV2nGbrlvGAQ",
        "CAACAgQAAxkBAAI5EF59vUKLQ46GEgbuzhY0O5r3HyauAAJKAQACqCEhBntEKK5RNh4XGAQ",
        "CAACAgIAAxkBAAI5El59vYqk6ywiJOKdXzXNe2gsPL2gAAL2AwACierlB263K9ogJ_bwGAQ",
        "CAACAgIAAxkBAAI5FF59vZFmQWYDsTaj4X9GJl9bPAbEAAJBAQAC-YQfHIsWbGjJcnqnGAQ",
        "CAACAgQAAxkBAAI5Fl59vb9993hlyxnbQ_VEZlEMqzymAAI7AgACMo1bAAEaE0PNwutkzBgE",
        "CAACAgIAAxkBAAI5GF59vfIG0bug-aIj8txxEBNNiNUXAAIQCAACGELuCOAfnJHe30ZuGAQ",
        "CAACAgIAAxkBAAI5Gl59vfg4AefyKXIXUAMOdoCs6gNAAALNBwACGELuCPlfWYiQZaQiGAQ"
    };
}