# This imports the client for sending responses and interacting with telegram
from Telegram.Bot import TelegramBotClientExtensions as Navigator
from Telegram.Bot.Types.InputFiles import InputOnlineFile


# Needed to understand if this module can handle the current message event
def can_handle_current_context(context, event):
    # Checks if the message exists (there are different types of events and usually we only care about message events.
    # In this particular case we also check if the message contains any text.
    if (event.Message is None) or (event.Message.Text is None):
        return False

    if "congrats" in event.Message.Text.lower() or "congratulations" in event.Message.Text.lower():
        return True

    return False


# Handler of the event, this is were the magic happens
def handle(conversation, event, args):
    # To send a sticker or any kind of image or gif, we need to construct an InputOnlineFile object.
    # It accepts media ids (stickers, gifs, previous images, etc.) and also accepts urls for images and gifs.
    sticker = InputOnlineFile("CAACAgIAAxkBAAEEZYRiTrRMXTzG543N43iKgLsPlyK_1wACwwcAAhhC7gjyFcEHatcw1CME")


    if event.Message.ReplyToMessage is None:
        Navigator.SendStickerAsync(args.client, args.chat, sticker)
    else:
        Navigator.SendStickerAsync(args.client, args.chat, sticker, replyToMessageId=event.Message.ReplyToMessage.MessageId)
