using FOSCBot.Core.Application.Abstractions;
using FOSCBot.Core.Application.Actions;
using FOSCBot.Core.Application.Services;
using FOSCBot.Core.Module.Options;
using FOSCBot.Infrastructure.Contracts.Clients;
using FOSCBot.Infrastructure.Implementations.Clients;
using FOSCBot.Infrastructure.Implementations.Services;
using Incremental.Common.Configuration;
using Lamar.Diagnostics;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.SemanticKernel;
using Navigator;
using Navigator.Configuration;
using Navigator.Configuration.Options;
using Navigator.Extensions.Cooldown;
using Navigator.Extensions.Probabilities;
using Navigator.Extensions.Store;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddCommonConfiguration();
// builder.Host.UseCommonLogging();
builder.Host.UseLamar();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddMemoryCache();
builder.Services.AddHybridCache();

// Semantic Kernel, AKA LLM
#pragma warning disable SKEXP0010
builder.Services.AddHttpClient("openwebui", (serviceProvider, client) =>
{
    client.BaseAddress = new Uri(builder.Configuration["AI_API_URL"]!);
});

builder.Services.AddTransient<Kernel>(serviceProvider =>
{
    var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

    return Kernel.CreateBuilder()
        .AddOpenAIChatCompletion(modelId: builder.Configuration["AI_CHAT_MODEL"]!, builder.Configuration["AI_API_KEY"]!,
            httpClient: clientFactory.CreateClient("openwebui"))
        .AddOpenAITextEmbeddingGeneration(modelId: builder.Configuration["AI_EMBEDDING_MODEL"]!, builder.Configuration["AI_API_KEY"]!,
            httpClient: clientFactory.CreateClient("openwebui"))
        .Build();
});

builder.Services.AddTransient<ProbabilityService>();
builder.Services.AddTransient<UnhingedService>();

builder.Services.Configure<FosboOptions>(builder.Configuration.GetSection("Fosbo"));

#region Navigator

builder.Services.AddNavigator(configuration =>
{
    configuration.Options.SetWebHookBaseUrl(builder.Configuration["BOT_URL"]!);
    configuration.Options.SetTelegramToken(builder.Configuration["TELEGRAM_TOKEN"]!);
    configuration.Options.EnableChatActionNotification();

    configuration.WithExtension<ProbabilitiesExtension>();
    configuration.WithExtension<CooldownExtension>();
    configuration.WithExtension<StoreExtension, StoreOptions>(options =>
    {
        options.ConfigureStore<>();
    });
});

#endregion

#region Infrastructure

builder.Services.AddOptions<BaconClient.BaconClientOptions>()
    .Configure(options => { options.ApiUrl = builder.Configuration["BACON_API_URL"]; });

builder.Services.AddOptions<MetaphorClient.MetaphorClientOptions>().Configure(options =>
{
    options.ApiUrl = builder.Configuration["METAPHOR_API_URL"];
});

builder.Services.AddOptions<InspiroClient.InspiroClientOptions>()
    .Configure(options => { options.ApiUrl = builder.Configuration["INSPIRO_API_URL"]; });

builder.Services.AddOptions<InsultClient.InsultClientOptions>()
    .Configure(options => { options.ApiUrl = builder.Configuration["INSULT_API_URL"]; });

builder.Services.AddOptions<YesNoClient.YesNoClientOptions>()
    .Configure(options => { options.ApiUrl = builder.Configuration["YESNO_API_URL"]; });

builder.Services.AddOptions<GiphyClient.GiphyClientOptions>().Configure(options =>
{
    options.ApiUrl = builder.Configuration["GIPHY_API_URL"];
    options.ApiKey = builder.Configuration["GIPHY_API_KEY"];
});

builder.Services.AddHttpClient<IBaconClient, BaconClient>()
    .AddTransientHttpErrorPolicy(builder =>
        builder.WaitAndRetryAsync(3, retryCount =>
            TimeSpan.FromSeconds(Math.Pow(2, retryCount))));

builder.Services.AddHttpClient<IMetaphorClient, MetaphorClient>()
    .AddTransientHttpErrorPolicy(builder =>
        builder.WaitAndRetryAsync(3, retryCount =>
            TimeSpan.FromSeconds(Math.Pow(2, retryCount))));

builder.Services.AddHttpClient<IInspiroClient, InspiroClient>()
    .AddTransientHttpErrorPolicy(builder =>
        builder.WaitAndRetryAsync(3, retryCount =>
            TimeSpan.FromSeconds(Math.Pow(2, retryCount))));

builder.Services.AddHttpClient<IInsultClient, InsultClient>()
    .AddTransientHttpErrorPolicy(builder =>
        builder.WaitAndRetryAsync(3, retryCount =>
            TimeSpan.FromSeconds(Math.Pow(2, retryCount))));

builder.Services.AddHttpClient<IYesNoClient, YesNoClient>()
    .AddTransientHttpErrorPolicy(builder =>
        builder.WaitAndRetryAsync(3, retryCount =>
            TimeSpan.FromSeconds(Math.Pow(2, retryCount))));

builder.Services.AddHttpClient<IGiphyClient, GiphyClient>()
    .AddTransientHttpErrorPolicy(builder =>
        builder.WaitAndRetryAsync(3, retryCount =>
            TimeSpan.FromSeconds(Math.Pow(2, retryCount))));

builder.Services.AddScoped<ILipsumService, LipsumService>();
builder.Services.AddScoped<IInspiroService, InspiroService>();
builder.Services.AddScoped<IInsultService, InsultService>();
builder.Services.AddScoped<IYesNoService, YesNoService>();
builder.Services.AddScoped<IGiphyService, GiphyService>();

#endregion

#region Healthchecks

builder.Services.AddHealthChecks();
builder.Services.CheckLamarConfiguration();

#endregion

var app = builder.Build();

var bot = app.GetBot();

bot.RegisterAdministration();
bot.RegisterCommands();
bot.RegisterInlineQueries();
#pragma warning disable SKEXP0001
bot.RegisterFallbacks();
#pragma warning restore SKEXP0001
bot.RegisterInteractivity();
bot.RegisterMiscellaneous();

app.MapNavigator();

app.MapHealthChecks("/health");

app.Run();