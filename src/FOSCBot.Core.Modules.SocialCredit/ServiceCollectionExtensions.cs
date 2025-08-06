using FOSCBot.Core.Modules.SocialCredit.Application.Abstractions.Infrastructure;
using FOSCBot.Core.Modules.SocialCredit.Application.Pipeline;
using FOSCBot.Core.Modules.SocialCredit.Application.Services;
using FOSCBot.Core.Modules.SocialCredit.Application.Services.Background;
using FOSCBot.Core.Modules.SocialCredit.Application.Services.Contracts;
using FOSCBot.Core.Modules.SocialCredit.Application.Services.Implementations;
using FOSCBot.Core.Modules.SocialCredit.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Extensions.Store.Services;

namespace FOSCBot.Core.Modules.SocialCredit;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Social Credit module services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSocialCreditModule(this IServiceCollection services)
    {
        services.AddOptions<MessageQueueServiceOptions>();
        services.AddSingleton<IMessageQueueService, MessageQueueService>();

        services.AddOptions<IntelligenceClientOptions>();
        services.AddScoped<IIntelligenceClient, IntelligenceClient>();
        
        services.AddScoped<IMessageScoringService, MessageScoringService>();
        services.AddScoped<ISocialCreditService, SocialCreditService>();

        services.AddHostedService<MessageProcessingBackgroundService>();

        services.AddScoped<INavigatorPipelineStep, BigBrotherPipelineStep>();
        
        return services;
    }
}