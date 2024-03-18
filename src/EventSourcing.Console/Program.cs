using EventSourcing.Infrastructure.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    static class Program
    {
        public static IConfigurationRoot Configuration { get; private set; }

        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            await host.RunAsync();

            Console.ReadKey();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
           .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
           {
               var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

               configurationBuilder
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{environment}.json", true, true)
                   .AddEnvironmentVariables();

               var configurationRoot = configurationBuilder.Build();
               Configuration = configurationRoot;

           }).ConfigureLogging((logging) =>
           {
               logging.AddSimpleConsole(o =>
               {
                   o.SingleLine = true;
               });

           }).ConfigureServices((services) =>
           {
               // IoC

               InjectorDependency.Register(services);

               services.AddHostedService<ConsoleApp>();
           });
    }
}
