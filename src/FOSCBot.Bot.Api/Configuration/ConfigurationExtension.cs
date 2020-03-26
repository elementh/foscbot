using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;

namespace FOSCBot.Bot.Api.Configuration
{
    public static class ConfigurationExtension
    {
        public static IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static ILogger LoadLogger(IConfiguration configuration)
        {
            var loggerConf = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                loggerConf.MinimumLevel.Debug();
            }
            else
            {
                loggerConf.MinimumLevel.Information();
            }

            return loggerConf.CreateLogger();
        }
    }
}