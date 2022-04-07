# This imports the client for sending responses and interacting with telegram.
from Telegram.Bot import TelegramBotClientExtensions as Navigator
# ParseMode import, explanation below.
from Telegram.Bot.Types.Enums import ParseMode
# This is a little helper that can be used to detect if the bot has been mentioned in the event.
from FOSCBot.Common.Helper import MentionHelper
# Usual python imports work too!
import random


# Needed to understand if this module can handle the current message event.
def can_handle_current_context(context, event):
    # Checks if the message exists (there are different types of events and usually we only care about message events).
    # In this particular case we also check if the message contains any text.
    if (event.Message is None) or (event.Message.Text is None):
        return False
    elif MentionHelper.IsBotMentioned(context) and "python" in event.Message.Text.lower():
        return True
    else:
        return False


# Handler of the event, this is were the magic happens.
def handle(conversation, event, args):
    i = random.randint(1, 100)

    if i >= 50:
        # A simple text message can be sent using Navigator, also there is an optional
        # parameter "replyToMessageId" that can be used to send the text message as a reply.
        Navigator.SendTextMessageAsync(args.client, args.chat,
                                       "Aprendí python para que los plebs puedan entenderme 🥱",
                                       replyToMessageId=event.Message.MessageId)
    else:
        # Another optional parameter is parseMode, which allows the use markdown syntax or html syntax.
        # Possible values are: ParseMode.MarkdownV2 or ParseMode.Html
        Navigator.SendTextMessageAsync(args.client, args.chat,
                                       "I'm `c̵̰̉ṳ̸̪̑̿̚̚r̴̟͕̗̄̓̀̉̄s̵̰̩̎̈e̷͙̯͍̲̋̊͘ḍ̴͂̊ ̶̘̈́a̷̹̲͗͗͊͋̾ṡ̸̨̢̰͂̀͗̕ ̵̧͓͑̈́̍͝f̷̦̠̹̤͊͛̈́u̸͇͔̓c̶̼͓͂̇k̴͍͐̽̏̈͗`, que alguien me desconecte joder 🤡🤡",
                                       parseMode=ParseMode.MarkdownV2,
                                       replyToMessageId=event.Message.MessageId)
