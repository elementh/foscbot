using System.Text.Json;
using FOSCBot.Infrastructure.Contracts.Clients;
using Microsoft.Extensions.Options;

namespace FOSCBot.Infrastructure.Implementations.Clients;

public class BaconClient : IBaconClient
{
    protected readonly HttpClient Client;
    protected readonly BaconClientOptions Options;

    public BaconClient(HttpClient client, IOptions<BaconClientOptions> options)
    {
        Client = client;
        Options = options.Value;

        Client.BaseAddress = new Uri(Options.ApiUrl);
    }

    public async Task<string> Get(string type, int sentences, CancellationToken cancellationToken = default)
    {
        var response = await Client.GetAsync($"api/?type={type}&sentences={sentences}", cancellationToken);

        response.EnsureSuccessStatusCode();

        var bacons = await JsonSerializer.DeserializeAsync<IEnumerable<string>>(await response.Content.ReadAsStreamAsync(), 
            cancellationToken: cancellationToken);

        return bacons.FirstOrDefault();
    }
        
    public class BaconClientOptions
    {
        public string ApiUrl { get; set; }
    }
}