using EventSourcing.Application.EventHandlers;
using EventSourcing.Application.CommandHandlers;
using EventSourcing.Domain.Contracts.Events;
using EventSourcing.Domain.Contracts.Commands;
using EventSourcing.Domain.Core.Common;
using EventSourcing.Infrastructure.Data.EventStores;
using EventSourcing.Infrastructure.Data.Repository;
using EventSourcing.Domain.AggregateModels.ShoppingCartAggregate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MediatR;
using EventSourcing.Domain.Contracts.Queries;
using EventSourcing.Application.QueryHandlers;

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
               // MediatR

               services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));

               // Application

               services.AddTransient<IRequestHandler<GetShoppingCartQuery, ShoppingCart>, GetShoppingCartQueryHandler>();
               services.AddTransient<IRequestHandler<CreateShoppingCartCommand, ShoppingCart>, CreateShoppingCartCommandHandler>();
          
               services.AddScoped<INotificationHandler<ShoppingCartCreatedEvent>, ShoppingCartCreatedEventHandler>();
               services.AddScoped<INotificationHandler<ItemAddedEvent>, ItemAddedEventHandler>();

               // Infrastructure

               services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
               services.AddTransient<IEventStore, SQLServerEventStore>();

               services.AddHostedService<ConsoleApp>();
           });
    }
}
