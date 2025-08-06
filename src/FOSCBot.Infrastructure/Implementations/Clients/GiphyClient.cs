using System.Text.Json;
using FOSCBot.Infrastructure.Contracts.Clients;
using FOSCBot.Infrastructure.Contracts.Models;
using Incremental.Common.Random;
using Microsoft.Extensions.Options;

namespace FOSCBot.Infrastructure.Implementations.Clients;

public class GiphyClient : IGiphyClient
{
    protected readonly HttpClient Client;
    protected readonly GiphyClientOptions Options;

    public GiphyClient(HttpClient client, IOptions<GiphyClientOptions> options)
    {
        Client = client;
        Options = options.Value;

        Client.BaseAddress = new Uri(Options.ApiUrl);
    }

    public async Task<Uri?> Get(string text, CancellationToken cancellationToken = default)
    {
        var response = await Client.GetAsync(
            $"v1/gifs/translate?api_key={Options.ApiKey}&s={text}&weirdness={RandomProvider.GetThreadRandom()!.Next(1, 10)}", cancellationToken);

        response.EnsureSuccessStatusCode();

        var data = await JsonSerializer.DeserializeAsync<GiphyResponseModel>(await response.Content.ReadAsStreamAsync(cancellationToken),
            cancellationToken: cancellationToken);

        if (data?.Data?.Images?.Original?.Mp4 is not null)
        {
            return new Uri(data.Data.Images.Original.Mp4);
        }

        return default;
    }

    public class GiphyClientOptions
    {
        public string ApiUrl { get; set; }
        public string ApiKey { get; set; }
    }
}