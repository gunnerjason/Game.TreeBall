using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using TreeBall.Services;

namespace TreeBall
{
    static class Program
    {
        public static void Main(String[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
 
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                EntryPoint app = serviceProvider.GetService<EntryPoint>();
                app?.Run();
            }

            Console.ReadKey();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
            IConfiguration configuration = builder.Build();
            services.AddSingleton(configuration);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddSerilog(logger: Log.Logger, dispose: true);
            });

            services.AddTransient<EntryPoint>()
                .AddScoped<IConduitGameService, ConduitGameService>();
 
            services.Configure<ConduitGameConfig>(configuration.GetSection("ConduitGame"));
        }
    }
}
