using FOSCBot.Core.Actions;
using FOSCBot.Core.Options;
using FOSCBot.Core.Services;
using FOSCBot.Infrastructure.Contract.Client;
using FOSCBot.Infrastructure.Contract.Service;
using FOSCBot.Infrastructure.Implementation.Client;
using FOSCBot.Infrastructure.Implementation.Service;
using Incremental.Common.Configuration;
using Lamar.Diagnostics;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.SemanticKernel;
using Navigator;
using Navigator.Configuration;
using Navigator.Configuration.Options;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddCommonConfiguration();
// builder.Host.UseCommonLogging();
builder.Host.UseLamar();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddMemoryCache();

// Semantic Kernel, AKA LLM
#pragma warning disable SKEXP0010
builder.Services.AddOpenAIChatCompletion(
    builder.Configuration["LLAMA_MODEL"],
    new Uri(builder.Configuration["LLAMA_API_URL"]));

builder.Services.AddTransient(serviceProvider => new Kernel(serviceProvider));

builder.Services.AddTransient<ProbabilityService>();
builder.Services.AddTransient<UnhingedService>();

builder.Services.Configure<FosboOptions>(builder.Configuration.GetSection("Fosbo"));

#region Navigator

builder.Services.AddNavigator(options =>
{
    options.SetWebHookBaseUrl(builder.Configuration["BOT_URL"]!);
    options.SetTelegramToken(builder.Configuration["TELEGRAM_TOKEN"]!);
    options.EnableChatActionNotification();
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

builder.Services.AddOptions<LlamaClient.LlamaClientOptions>().Configure(options =>
{
    options.ApiUrl = builder.Configuration["LLAMA_API_URL"];
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

builder.Services.AddHttpClient<ILlamaClient, LlamaClient>()
    .AddTransientHttpErrorPolicy(builder =>
        builder.WaitAndRetryAsync(3, retryCount =>
            TimeSpan.FromSeconds(Math.Pow(2, retryCount))));

builder.Services.AddHttpClient<IMemeClient, MemeClient>()
    .AddTransientHttpErrorPolicy(builder =>
        builder.WaitAndRetryAsync(3, retryCount =>
            TimeSpan.FromSeconds(Math.Pow(2, retryCount))));

builder.Services.AddScoped<ILipsumService, LipsumService>();
builder.Services.AddScoped<IInspiroService, InspiroService>();
builder.Services.AddScoped<IInsultService, InsultService>();
builder.Services.AddScoped<IYesNoService, YesNoService>();
builder.Services.AddScoped<IGiphyService, GiphyService>();
builder.Services.AddScoped<ILlamaService, LlamaService>();
builder.Services.AddScoped<IMemeService, MemeService>();

#endregion

#region Healthchecks

builder.Services.AddHealthChecks();
builder.Services.CheckLamarConfiguration();

#endregion

var app = builder.Build();

var bot = app.GetBot();

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