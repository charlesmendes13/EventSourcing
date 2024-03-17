using EventSourcing.Domain.AggregateModels;
using EventSourcing.Infrastructure.Ioc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Program
{
    static class Program
    {
        public static IConfigurationRoot Configuration { get; private set; }

        static void Main(string[] args)
        {
            var builder = new HostBuilder().ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                configurationBuilder
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environment}.json", true, true)
                    .AddEnvironmentVariables();

                var configurationRoot = configurationBuilder.Build();
                Configuration = configurationRoot;

            })
            .ConfigureServices((hostContext, services) =>
            {
                // IoC
                InjectorDependency.Register(services);

            })
            .UseConsoleLifetime();

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var repository = services.GetRequiredService<IShoppingCartRepository>();

                // Criando um novo carrinho e adicionando itens
                var shoppingCart = new ShoppingCart(Guid.NewGuid(), "Alice");
                shoppingCart.AddItem(Guid.NewGuid(), "Product A", 10.99m);
                shoppingCart.AddItem(Guid.NewGuid(), "Product B", 20.49m);

                // Salvando o carrinho no repositório
                repository.Save(shoppingCart);

                // Recuperando o carrinho do repositório
                var retrievedCart = repository.GetById(shoppingCart.Id);

                // Verificando se o carrinho foi recuperado corretamente
                Console.WriteLine($"Customer: {retrievedCart.CustomerName}");
                Console.WriteLine("==== Items ====");
                foreach (var item in retrievedCart.Items)
                {
                    Console.WriteLine($"ItemName: {item}");
                }

                Console.ReadLine();
            }
        }
    }
}
