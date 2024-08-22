using FOSCBot.Core.Resources;
using Incremental.Common.Random;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Navigator.Client;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FOSCBot.Core.Actions;

public static class Photos
{
    public static void RegisterPhotos(this BotActionCatalogFactory catalog)
    {
        catalog
            .OnSticker((Sticker sticker) => sticker.SetName?.Equals("foscupct") is not false && sticker.Emoji?.Equals("😚") is true,
                async (INavigatorClient client, Chat chat) =>
                {
                    var bytes = Convert.FromBase64String(CoreResources.HeyBroImage);

                    await using var stream = await new StreamContent(new MemoryStream(bytes)).ReadAsStreamAsync();

                    await client.SendPhotoAsync(chat, new InputFileStream(stream, "heybroniced.jpg"));
                })
            .WithChances(0.5);
    }
}