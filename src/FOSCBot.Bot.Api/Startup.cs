using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FOSCBot.Core.Domain.Inline;
using FOSCBot.Core.Domain.Inline.Default;
using FOSCBot.Infrastructure.Contract.Client;
using FOSCBot.Infrastructure.Contract.Service;
using FOSCBot.Infrastructure.Implementation.Client;
using FOSCBot.Infrastructure.Implementation.Service;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Navigator;
using Polly;

namespace FOSCBot.Bot.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            
            #region Navigator

            services.AddNavigator(options =>
            {
                options.BotToken = Configuration["TELEGRAM_TOKEN"];
                options.BaseWebHookUrl = Configuration["BOT_URL"];
                options.MultipleLaunchEnabled = false;
            }, typeof(DefaultInlineAction).Assembly);

            #endregion

            #region Pipeline

            services.AddMediatR(typeof(DefaultInlineAction).Assembly);

            #endregion

            #region Infrastructure

            services.AddOptions<BaconClient.BaconClientOptions>().Configure(options =>
            {
                options.ApiUrl = Configuration["BACON_API_URL"];
            });
            
            services.AddOptions<MetaphorClient.MetaphorClientOptions>().Configure(options =>
            {
                options.ApiUrl = Configuration["METAPHOR_API_URL"];
            });
            
            services.AddHttpClient<IBaconClient, BaconClient>()
                .AddTransientHttpErrorPolicy(builder =>
                    builder.WaitAndRetryAsync(3, retryCount =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryCount))));
            
            services.AddHttpClient<IMetaphorClient, MetaphorClient>()
                .AddTransientHttpErrorPolicy(builder =>
                    builder.WaitAndRetryAsync(3, retryCount =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryCount))));

            services.AddScoped<ILipsumService, LipsumService>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 
                endpoints.MapNavigator();
            });
        }
    }
}