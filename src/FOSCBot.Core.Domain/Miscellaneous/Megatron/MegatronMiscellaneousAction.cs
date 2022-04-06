using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace FOSCBot.Core.Domain.Miscellaneous.Megatron;

public class MegatronMiscellaneousAction : MessageAction
{
    public override bool CanHandleCurrentContext()
    {
        return  (Message?.Text?.ToLower().Contains("megatron") ?? false) &&
                (!Message?.Text?.ToLower().ContainsUrl() ?? false);
    }
}