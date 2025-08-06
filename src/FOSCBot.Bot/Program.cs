using FOSCBot.Bot.Configuration;
using FOSCBot.Common.Persistence;
using FOSCBot.Core.Application.Abstractions;
using FOSCBot.Core.Application.Actions;
using FOSCBot.Core.Application.Services;
using FOSCBot.Core.Module.Options;
using FOSCBot.Core.Modules.SocialCredit;
using FOSCBot.Core.Modules.SocialCredit.Application.Abstractions.Persistence;
using FOSCBot.Infrastructure.Contracts.Clients;
using FOSCBot.Infrastructure.Implementations.Clients;
using FOSCBot.Infrastructure.Implementations.Services;
using FOSCBot.Persistence.Context;
using Incremental.Common.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using Navigator;
using Navigator.Configuration;
using Navigator.Configuration.Options;
using Navigator.Extensions.Cooldown;
using Navigator.Extensions.Probabilities;
using Navigator.Extensions.Store;
using Navigator.Extensions.Store.Services;
using Npgsql;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddCommonConfiguration();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddMemoryCache();
builder.Services.AddHybridCache();

builder.Services.AddTransient<ProbabilityService>();
builder.Services.AddTransient<UnhingedService>();

builder.Services.Configure<FosboOptions>(builder.Configuration.GetSection("Fosbo"));

builder.AddIntelligence();

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
        var connectionString = builder.Configuration.GetConnectionString("FosboDb");

        if (string.IsNullOrEmpty(connectionString))
        {
                
            var host = builder.Configuration["PG_HOST"];
            var user = builder.Configuration["PG_USER"];
            var password = builder.Configuration["PG_PW"];

            var connectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Username = user,
                Password = password,
                Database = "foscbot",
                Port = 5432,
                SslMode = SslMode.Prefer,
            };
                
            connectionString = connectionStringBuilder.ToString();
        }

        options.ConfigureStore<FosboDbContext>(dbBuilder =>
        {
            dbBuilder.UseNpgsql(connectionString);
        });
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

#region Modules

// Add Social Credit module
builder.Services.AddSocialCreditModule();

// Register additional database interfaces for Social Credit module
builder.Services.AddScoped<ISocialCreditDbContext, FosboDbContext>();
builder.Services.AddScoped<IFosboDbContext, FosboDbContext>();

#endregion

#region Healthchecks

builder.Services.AddHealthChecks();

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

using var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
serviceScope?.ServiceProvider.GetRequiredService<FosboDbContext>().Database.Migrate();

app.Run();