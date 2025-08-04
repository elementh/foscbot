﻿using FOSCBot.Infrastructure.Contracts.Clients;
using Microsoft.Extensions.Options;

namespace FOSCBot.Infrastructure.Implementations.Clients;

public class InspiroClient : IInspiroClient
{
    protected readonly HttpClient Client;
    protected readonly InspiroClientOptions Options;

    public InspiroClient(HttpClient client, IOptions<InspiroClientOptions> options)
    {
        Client = client;
        Options = options.Value;

        Client.BaseAddress = new Uri(Options.ApiUrl);
    }

    public async Task<string> Get(CancellationToken cancellationToken = default)
    {
        var response = await Client.GetAsync("api?generate=true", cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public class InspiroClientOptions
    {
        public string ApiUrl { get; set; }
    }
}