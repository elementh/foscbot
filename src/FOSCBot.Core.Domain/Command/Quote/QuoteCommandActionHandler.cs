﻿using FOSCBot.Infrastructure.Contract.Service;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram;
using Telegram.Bot;

namespace FOSCBot.Core.Domain.Command.Quote;

public class QuoteCommandActionHandler : ActionHandler<QuoteCommandAction>
{
    private readonly IInspiroService _inspiroService;

    public QuoteCommandActionHandler(INavigatorContextAccessor navigatorContextAccessor, IInspiroService inspiroService) : base(navigatorContextAccessor)
    {
        _inspiroService = inspiroService;
    }

    public override async Task<Status> Handle(QuoteCommandAction action, CancellationToken cancellationToken)
    {
        var image = await _inspiroService.GetInspiroImage(cancellationToken);

        await NavigatorContext.GetTelegramClient().SendPhotoAsync(NavigatorContext.GetTelegramChat()!, image, cancellationToken: cancellationToken);
            
        return Success();
    }
}