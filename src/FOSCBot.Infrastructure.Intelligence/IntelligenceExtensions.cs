using FOSCBot.Core.Application.Abstractions;
using FOSCBot.Infrastructure.Intelligence.Abstractions.Client;
using FOSCBot.Infrastructure.Intelligence.Client;
using FOSCBot.Infrastructure.Intelligence.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace FOSCBot.Infrastructure.Intelligence;

public static class IntelligenceExtensions
{
    public static IServiceCollection AddIntelligence(this IServiceCollection services, Action<IntelligenceOptions> configure)
    {
        var options = new IntelligenceOptions();
        configure(options);

        foreach (var provider in options.ChatCompletionProviders)
        {
            services.AddHttpClient(provider.GetClientName(), (_, client) =>
            {
                client.BaseAddress = new Uri(provider.ApiUrl);

                if (!string.IsNullOrEmpty(provider.ApiKey))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {provider.ApiKey}");
                }
            });
        }

        services.AddTransient<Kernel>(serviceProvider =>
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

        services.AddTransient<UnhingedService>();
        services.AddTransient<IUnhingedService>(sp => sp.GetRequiredService<UnhingedService>());
        services.AddSingleton<ISilenceService, SilenceService>();
        services.AddScoped<IAgentService, AgentService>();
        services.AddScoped<ITextMeaningService, TextMeaningService>();
        services.AddScoped<ICommandSynthesizerService, CommandSynthesizerService>();
        services.AddScoped<IPhantomCommandService, PhantomCommandService>();
        services.AddScoped<IAdminAuthService, AdminAuthService>();

        services.AddOptions<IntelligenceClientOptions>();
        services.AddScoped<IIntelligenceClient, IntelligenceClient>();

        return services;
    }
}

public class IntelligenceOptions
{
    public const string Key = "Intelligence";
    public Provider[] ChatCompletionProviders { get; set; } = [];
    
    public class Provider
    {
        public required string Name { get; init; }
        public required string ModelId { get; init; }
        public required string ApiUrl { get; init; }
        public string? ApiKey { get; init; }
        
        public string GetClientName() => $"{Name}_client";
    }
}
