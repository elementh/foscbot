# This imports the client for sending responses and interacting with telegram
from Telegram.Bot import TelegramBotClientExtensions as Navigator
from Telegram.Bot.Types.InputFiles import InputOnlineFile
import System

# Needed to understand if this module can handle the current message event
def can_handle_current_context(context, event):
    # Checks if the message exists (there are different types of events and usually we only care about message events.
    # In this particular case we also check if the message contains any text.
    if (event.Message is None) or (event.Message.Text is None):
        return False
    elif "monke" in event.Message.Text.lower():
        return True
    else:
        return False


# Handler of the event, this is were the magic happens
def handle(conversation, event, args):
    # To send a sticker or any kind of image or gif, we need to construct an InputOnlineFile object.
    # It accepts media ids (stickers, gifs, previous images, etc.) and also accepts urls for images and gifs.
    photo = InputOnlineFile("https://raw.githubusercontent.com/elementh/foscbot/main/assets/monke.gif")
    Navigator.SendPhotoAsync(args.client, args.chat, photo, caption="RETURN TO MONKE FTW")
