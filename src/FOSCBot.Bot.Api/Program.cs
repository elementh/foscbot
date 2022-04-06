using System.Reflection;
using Ele.Extensions.Configuration;
using Serilog;

namespace FOSCBot.Bot.Api;

public class Program
{
    private static IConfiguration Configuration { get; } = ConfigurationExtension.LoadConfiguration(Directory.GetCurrentDirectory());
        
    public static void Main(string[] args)
    {
        var assembly = Assembly.GetCallingAssembly().GetName().Name;

        Log.Logger = ConfigurationExtension.LoadLogger(Configuration);
        try
        {
            var host = CreateHostBuilder(args).Build();

            Log.Information($"Starting {assembly}");

            host.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, $"{assembly} terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
        
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseSerilog();
            });
}