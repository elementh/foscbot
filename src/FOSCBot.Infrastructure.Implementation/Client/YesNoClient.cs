using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Infrastructure.Contract.Client;
using FOSCBot.Infrastructure.Contract.Model;
using Microsoft.Extensions.Options;

namespace FOSCBot.Infrastructure.Implementation.Client;

public class YesNoClient : IYesNoClient
{
    protected readonly HttpClient Client;
    protected readonly YesNoClientOptions Options;

    public YesNoClient(HttpClient client, IOptions<YesNoClientOptions> options)
    {
        Client = client;
        Options = options.Value;

        Client.BaseAddress = new Uri(Options.ApiUrl);
    }

    public async Task<YesNoAnswerModel> GetAnswer(string type, CancellationToken cancellationToken = default)
    {
        var response = await Client.GetAsync($"api?force={type}", cancellationToken);

        response.EnsureSuccessStatusCode();
            
        var answer = await JsonSerializer.DeserializeAsync<YesNoAnswerModel>(await response.Content.ReadAsStreamAsync(),
            cancellationToken: cancellationToken);

        return answer;
    }

    public class YesNoClientOptions
    {
        public string ApiUrl { get; set; }
    }
}