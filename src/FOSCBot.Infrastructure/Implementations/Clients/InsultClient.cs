using FOSCBot.Infrastructure.Contracts.Clients;
using Microsoft.Extensions.Options;

namespace FOSCBot.Infrastructure.Implementations.Clients;

public class InsultClient : IInsultClient
{
    protected readonly HttpClient Client;
    protected readonly InsultClientOptions Options;

    public InsultClient(HttpClient client, IOptions<InsultClientOptions> options)
    {
        Client = client;
        Options = options.Value;

        Client.BaseAddress = new Uri(Options.ApiUrl);
    }
    public async Task<string> Get(CancellationToken cancellationToken = default)
    {
        var response = await Client.GetAsync("generate_insult.php?lang=en", cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public class InsultClientOptions
    {
        public string ApiUrl { get; set; }
    }
}