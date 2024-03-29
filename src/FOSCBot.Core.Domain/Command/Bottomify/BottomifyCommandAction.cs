﻿using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Command.Bottomify;

public class BottomifyCommandAction : CommandAction
{
    public BottomifyCommandAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }
    
    public override bool CanHandleCurrentContext()
    {
        return Command.ToLower() == "/bottomify";
    }
}