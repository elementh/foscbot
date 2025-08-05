using Microsoft.SemanticKernel;

namespace FOSCBot.Bot.Configuration;

public static class IntelligenceExtensions
{
    public static void AddIntelligence(this WebApplicationBuilder builder)
    {
        var options = builder.Configuration.GetSection(IntelligenceOptions.Key)
            .Get<IntelligenceOptions>() ?? throw new InvalidOperationException($"Failed to bind {IntelligenceOptions.Key}.");

        foreach (var provider in options.ChatCompletionProviders)
        {

            builder.Services.AddHttpClient(provider.GetClientName(), (_, client) =>
            {
                client.BaseAddress = new Uri(provider.ApiUrl);

                if (!string.IsNullOrEmpty(provider.ApiKey))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {provider.ApiKey}");
                }
            });
        }

        builder.Services.AddTransient<Kernel>(serviceProvider =>
        {
            var kernelBuilder = Kernel.CreateBuilder();
            var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

            foreach (var provider in options.ChatCompletionProviders)
            {
                kernelBuilder.AddOllamaChatCompletion(
                    modelId: provider.ModelId,
                    httpClient: clientFactory.CreateClient(provider.GetClientName()),
                    serviceId: provider.Name);
            }

            return kernelBuilder.Build();
        });
    }
}

public class IntelligenceOptions
{
    public const string Key = "Intelligence";
    public required Provider[] ChatCompletionProviders { get; init; }
    
    public class Provider
    {
        public required string Name { get; init; }
        public required string ModelId { get; init; }
        public required string ApiUrl { get; init; }
        public string? ApiKey { get; init; }
        
        public string GetClientName() => $"{Name}_client";
    }
}