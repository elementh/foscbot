using FOSCBot.Core.Application.Abstractions;
using FOSCBot.Infrastructure.Intelligence.Abstractions.Client;
using FOSCBot.Infrastructure.Intelligence.Client;
using FOSCBot.Infrastructure.Intelligence.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace FOSCBot.Infrastructure.Intelligence;

public static class IntelligenceExtensions
{
    public static IServiceCollection AddIntelligence(this IServiceCollection services, Action<IntelligenceOptions> configure)
    {
        var options = new IntelligenceOptions();
        configure(options);

        services.AddTransient<LoggingHandler>();

        foreach (var provider in options.ChatCompletionProviders)
        {
            services.AddHttpClient(provider.GetClientName(), (_, client) =>
            {
                client.BaseAddress = new Uri(provider.ApiUrl);

                if (!string.IsNullOrEmpty(provider.ApiKey) && provider.ProviderType == ProviderType.Ollama)
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {provider.ApiKey}");
                }

                if (!string.IsNullOrEmpty(options.SecHName) && !string.IsNullOrEmpty(options.SecHVal))
                {
                    client.DefaultRequestHeaders.Add(options.SecHName, options.SecHVal);
                }
            })
            .AddHttpMessageHandler<LoggingHandler>();
        }

        services.AddTransient<Kernel>(serviceProvider =>
        {
            var kernelBuilder = Kernel.CreateBuilder();
            var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

            foreach (var provider in options.ChatCompletionProviders)
            {
                if (provider.ProviderType == ProviderType.Ollama)
                {
                    kernelBuilder.AddOllamaChatCompletion(
                        modelId: provider.ModelId,
                        httpClient: clientFactory.CreateClient(provider.GetClientName()),
                        serviceId: provider.Name);
                }
                else if (provider.ProviderType == ProviderType.OpenAICompatible)
                {
                    kernelBuilder.AddOpenAIChatCompletion(
                        modelId: provider.ModelId,
                        apiKey: provider.ApiKey ?? string.Empty,
                        httpClient: clientFactory.CreateClient(provider.GetClientName()),
                        serviceId: provider.Name);
                }
            }

            return kernelBuilder.Build();
        });

        services.AddTransient<UnhingedService>();
        services.AddTransient<IUnhingedService>(sp => sp.GetRequiredService<UnhingedService>());
        services.AddSingleton<ISilenceService, SilenceService>();
        services.AddScoped<IAgentService, AgentService>();
        services.AddScoped<ITextMeaningService, TextMeaningService>();
        services.AddScoped<ICommandSynthesizerService, CommandSynthesizerService>();
        services.AddScoped<IPhantomCommandService, PhantomCommandService>();
        services.AddScoped<IAdminAuthService, AdminAuthService>();
        services.AddScoped<IUserFallbackService, UserFallbackService>();

        services.AddOptions<IntelligenceClientOptions>();
        services.AddScoped<IIntelligenceClient, IntelligenceClient>();

        return services;
    }
}

public enum ProviderType
{
    Ollama,
    OpenAICompatible
}

public class IntelligenceOptions
{
    public const string Key = "Intelligence";
    public Provider[] ChatCompletionProviders { get; set; } = [];
    public string? SecHName { get; set; }
    public string? SecHVal { get; set; }
    
    public class Provider
    {
        public required string Name { get; init; }
        public required string ModelId { get; init; }
        public required string ApiUrl { get; init; }
        public string? ApiKey { get; init; }
        public ProviderType ProviderType { get; init; } = ProviderType.Ollama;
        
        public string GetClientName() => $"{Name}_client";
    }
}
