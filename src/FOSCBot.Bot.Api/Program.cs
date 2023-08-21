using FOSCBot.Bot.Api.Health;
using FOSCBot.Common.Pipeline;
using FOSCBot.Core.Domain.Inline.Default;
using FOSCBot.Infrastructure.Contract.Client;
using FOSCBot.Infrastructure.Contract.Service;
using FOSCBot.Infrastructure.Implementation.Client;
using FOSCBot.Infrastructure.Implementation.Service;
using Incremental.Common.Configuration;
using Incremental.Common.Logging;
using Lamar.Diagnostics;
using Lamar.Microsoft.DependencyInjection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Navigator;
using Navigator.Configuration;
using Navigator.Extensions.Cooldown;
using Navigator.Extensions.Interop;
using Navigator.Extensions.Store;
using Navigator.Extensions.Store.Context;
using Navigator.Extensions.Store.Context.Extension;
using Navigator.Extensions.Store.Telegram;
using Navigator.Providers.Telegram;
using Polly;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddCommonConfiguration();
builder.Host.UseCommonLogging();
builder.Host.UseLamar();

builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddDistributedMemoryCache();

#region Navigator

builder.Services.AddNavigator(options =>
    {
        options.SetWebHookBaseUrl(builder.Configuration["BOT_URL"]);
        options.RegisterActionsFromAssemblies(typeof(DefaultInlineAction).Assembly);
    }).WithProvider.Telegram(options => { options.SetTelegramToken(builder.Configuration["TELEGRAM_TOKEN"]); })
    // .WithExtension.Interop(options => { options.Runtime = builder.Configuration["INTEROP_PYTHON"]; })
    .WithExtension.Store(dbBuilder =>
    {
        dbBuilder.UseNpgsql(builder.Configuration["DB_CONNECTION_STRING"],
            dbContextOptionsBuilder => { dbContextOptionsBuilder.MigrationsAssembly("FOSCBot.Persistence.Migrations"); });
    }).WithExtension.StoreForTelegram()
    .WithExtension.Cooldown();

#endregion

#region Pipeline

builder.Services.AddScoped<Watcher, Watcher>();

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipeline<,>));

#endregion

#region Infrastructure

builder.Services.AddOptions<BaconClient.BaconClientOptions>().Configure(options => { options.ApiUrl =builder.Configuration["BACON_API_URL"]; });

builder.Services.AddOptions<MetaphorClient.MetaphorClientOptions>().Configure(options => { options.ApiUrl =builder.Configuration["METAPHOR_API_URL"]; });

builder.Services.AddOptions<InspiroClient.InspiroClientOptions>()
    .Configure(options => { options.ApiUrl =builder.Configuration["INSPIRO_API_URL"]; });

builder.Services.AddOptions<InsultClient.InsultClientOptions>().Configure(options => { options.ApiUrl =builder.Configuration["INSULT_API_URL"]; });

builder.Services.AddOptions<YesNoClient.YesNoClientOptions>().Configure(options => { options.ApiUrl =builder.Configuration["YESNO_API_URL"]; });

builder.Services.AddOptions<GiphyClient.GiphyClientOptions>().Configure(options =>
{
    options.ApiUrl =builder.Configuration["GIPHY_API_URL"];
    options.ApiKey =builder.Configuration["GIPHY_API_KEY"];
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

builder.Services.AddScoped<ILipsumService, LipsumService>();
builder.Services.AddScoped<IInspiroService, InspiroService>();
builder.Services.AddScoped<IInsultService, InsultService>();
builder.Services.AddScoped<IYesNoService, YesNoService>();
builder.Services.AddScoped<IGiphyService, GiphyService>();
builder.Services.AddScoped<ILlamaService, LlamaService>();

#endregion

#region Healthchecks

builder.Services.AddHealthChecks()
    .AddCheck<PythonHealthCheck>("Python");
builder.Services.CheckLamarConfiguration();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

using var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
serviceScope?.ServiceProvider.GetRequiredService<NavigatorDbContext>().Database.Migrate();

app.MapNavigator()
    .ForProvider.Telegram();

app.MapHealthChecks("/health");

app.Run();