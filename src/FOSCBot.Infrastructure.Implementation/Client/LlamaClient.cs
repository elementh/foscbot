using System.Net.Http.Json;
using System.Text.Json.Serialization;
using FOSCBot.Infrastructure.Contract.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace FOSCBot.Infrastructure.Implementation.Client;

public class LlamaClient: ILlamaClient
{
    protected readonly HttpClient Client;
    protected readonly LlamaClientOptions Options;

    public LlamaClient(HttpClient client, IOptions<LlamaClientOptions> options)
    {
        Client = client;
        Options = options.Value;

        Client.BaseAddress = new Uri(Options.ApiUrl);
    }

    public async Task<string?> GetChatCompletions(string chat, string? prompt)
    {
        var response = await Client.PostAsJsonAsync<LlamaRequest>("/v1/chat/completions", new LlamaRequest()
        {
            Messages = new []
            {
                new LlamaRequest.Message()
                {
                    Role = "system",
                    Content = prompt
                },
                new LlamaRequest.Message()
                {
                    Role = "user",
                    Content = chat
                }
            },
            Max_Tokens = 4000,
            Temperature = 0.9
        });

        var content = await response.Content.ReadFromJsonAsync<LlamaResponse>();

        return content?.Choices.First().Message.Content?.Replace('#', default);
    }
    
    public class LlamaClientOptions
    {
        public string ApiUrl { get; set; }
    }

    public class LlamaRequest
    {
        public Message[] Messages { get; set; }
        [JsonPropertyName("max_tokens")]
        public int Max_Tokens { get; set; } = 4000;
        [JsonPropertyName("temperature")]
        public double Temperature { get; set; } = 1;
            
            
        public class Message
        {
            public string Role { get; set; }
            public string? Content { get; set; }
        }
    }

    public class LlamaResponse
    {
        public Choice[] Choices { get; set; }

        public class Choice
        {
            public LlamaRequest.Message Message { get; set; }
        }
    }
}