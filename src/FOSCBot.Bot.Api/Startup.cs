using Ele.Extensions.Configuration;
using FOSCBot.Common.Pipeline;
using FOSCBot.Core.Domain.Inline.Default;
using FOSCBot.Infrastructure.Contract.Client;
using FOSCBot.Infrastructure.Contract.Service;
using FOSCBot.Infrastructure.Implementation.Client;
using FOSCBot.Infrastructure.Implementation.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Navigator;
using Navigator.Configuration;
using Navigator.Extensions.Store.Context;
using Polly;

namespace FOSCBot.Bot.Api;

public class Startup
{
    public Startup()
    {
        Configuration = ConfigurationExtension.LoadConfiguration(Directory.GetCurrentDirectory());
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson();

        services.AddApiVersioning();

        services.AddHealthChecks();

        services.AddMemoryCache();

        #region Navigator

        services.AddNavigator(options =>
        {
            options.SetTelegramToken(Configuration["TELEGRAM_TOKEN"]);
            options.SetWebHookBaseUrl(Configuration["BOT_URL"]);
            options.RegisterActionsFromAssemblies(typeof(DefaultInlineAction).Assembly);
        }).AddNavigatorStore(builder =>
        {
            builder.UseNpgsql(Configuration["DB_CONNECTION_STRING"],
                b => b.MigrationsAssembly("FOSCBot.Persistence.Migrations"));
        }).AddShipyard(options =>
        {
            options.SetShipyardApiKey(Configuration["SHIPYARD_API_KEY"]);
        });

        #endregion

        #region Pipeline
            
        services.AddScoped<Watcher, Watcher>();

        services.AddMediatR(typeof(DefaultInlineAction).Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipeline<,>));

        #endregion

        #region Infrastructure

        services.AddOptions<BaconClient.BaconClientOptions>().Configure(options => { options.ApiUrl = Configuration["BACON_API_URL"]; });

        services.AddOptions<MetaphorClient.MetaphorClientOptions>().Configure(options => { options.ApiUrl = Configuration["METAPHOR_API_URL"]; });

        services.AddOptions<InspiroClient.InspiroClientOptions>().Configure(options => { options.ApiUrl = Configuration["INSPIRO_API_URL"]; });

        services.AddOptions<InsultClient.InsultClientOptions>().Configure(options => { options.ApiUrl = Configuration["INSULT_API_URL"]; });

        services.AddOptions<YesNoClient.YesNoClientOptions>().Configure(options => { options.ApiUrl = Configuration["YESNO_API_URL"]; });

        services.AddOptions<GiphyClient.GiphyClientOptions>().Configure(options =>
        {
            options.ApiUrl = Configuration["GIPHY_API_URL"];
            options.ApiKey = Configuration["GIPHY_API_KEY"];
        });

        services.AddHttpClient<IBaconClient, BaconClient>()
            .AddTransientHttpErrorPolicy(builder =>
                builder.WaitAndRetryAsync(3, retryCount =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryCount))));

        services.AddHttpClient<IMetaphorClient, MetaphorClient>()
            .AddTransientHttpErrorPolicy(builder =>
                builder.WaitAndRetryAsync(3, retryCount =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryCount))));

        services.AddHttpClient<IInspiroClient, InspiroClient>()
            .AddTransientHttpErrorPolicy(builder =>
                builder.WaitAndRetryAsync(3, retryCount =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryCount))));
            
        services.AddHttpClient<IInsultClient, InsultClient>()
            .AddTransientHttpErrorPolicy(builder =>
                builder.WaitAndRetryAsync(3, retryCount =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryCount))));
            
        services.AddHttpClient<IYesNoClient, YesNoClient>()
            .AddTransientHttpErrorPolicy(builder =>
                builder.WaitAndRetryAsync(3, retryCount =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryCount))));
            
        services.AddHttpClient<IGiphyClient, GiphyClient>()
            .AddTransientHttpErrorPolicy(builder =>
                builder.WaitAndRetryAsync(3, retryCount =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryCount))));

        services.AddScoped<ILipsumService, LipsumService>();
        services.AddScoped<IInspiroService, InspiroService>();
        services.AddScoped<IInsultService, InsultService>();
        services.AddScoped<IYesNoService, YesNoService>();
        services.AddScoped<IGiphyService, GiphyService>();

        #endregion
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
            
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
        serviceScope?.ServiceProvider.GetRequiredService<NavigatorDbContext>().Database.Migrate();

        app.UseRouting();
            
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapNavigator();
            endpoints.MapHealthChecks("/health");
        });
    }
}