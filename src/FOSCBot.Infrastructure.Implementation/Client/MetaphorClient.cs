using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FOSCBot.Infrastructure.Contract.Client;
using Microsoft.Extensions.Options;

namespace FOSCBot.Infrastructure.Implementation.Client
{
    public class MetaphorClient : IMetaphorClient
    {
        protected readonly HttpClient Client;
        protected readonly MetaphorClientOptions Options;

        public MetaphorClient(HttpClient client, IOptions<MetaphorClientOptions> options)
        {
            Client = client;
            Options = options.Value;

            Client.BaseAddress = new Uri(Options.ApiUrl);
        }

        public async Task<string> GetSentence(int quantity, CancellationToken cancellationToken = default)
        {
            var response = await Client.GetAsync($"sentences/{quantity}", cancellationToken);
            
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetParagraph(int quantity, CancellationToken cancellationToken = default)
        {
            var response = await Client.GetAsync($"paragraphs/{quantity}", cancellationToken);
            
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        
        
        public class MetaphorClientOptions
        {
            public string ApiUrl { get; set; }
        }
    }
}