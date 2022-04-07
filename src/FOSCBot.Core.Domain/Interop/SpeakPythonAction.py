# This imports the client for sending responses and interacting with telegram
from Telegram.Bot import TelegramBotClientExtensions as Navigator

# This is a little helper that can be used to detect if the bot has been mentioned in the event
from FOSCBot.Common.Helper import MentionHelper


# Needed to understand if this module can handle the current message event
def can_handle_current_context(context, event):
    # Checks if the message exists (there are different types of events and usually we only care about message events.
    # In this particular case we also check if the message contains any text.
    if (event.Message is None) or (event.Message.Text is None):
        return False

    if MentionHelper.IsBotMentioned(context) and "python" in event.Message.Text.lower():
        return True

    return False


# Handler of the event, this is were the magic happens
def handle(conversation, event, args):
    # A simple text message can be sent using Navigator, also there is an optional parameter "replyToMessageId" that can be
    # used to send the text message as a reply.
    Navigator.SendTextMessageAsync(args.client, args.chat, "Aprendí python para que los plebs puedan entenderme 🥱",
                                   replyToMessageId=event.Message.MessageId)
