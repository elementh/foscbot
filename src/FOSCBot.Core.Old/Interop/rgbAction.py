from Telegram.Bot import TelegramBotClientExtensions as Navigator
import System


# Needed to understand if this module can handle the current message event.
def can_handle_current_context(context, event):
    if (event.Message is None) or (event.Message.Text is None):
        return False
    elif "rgb" in event.Message.Text.lower():
        return True
    else:
        return False


# Handler of the event, this is were the magic happens.
def handle(conversation, event, args):
    Navigator.SendTextMessageAsync(args.client, args.chat, "eL rGb n0 Da FpS")