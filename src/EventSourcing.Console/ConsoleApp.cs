using EventSourcing.Domain.Contracts.Commands;
using EventSourcing.Domain.Contracts.Queries;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;

namespace ConsoleApp
{
    public class ConsoleApp : IHostedService
    {
        private readonly IHostApplicationLifetime _appLifeTime;
        private readonly ILogger<ConsoleApp> _logger;
        private readonly IMediator _mediator;

        public ConsoleApp(IHostApplicationLifetime appLifeTime,
            ILogger<ConsoleApp> logger,
            IMediator mediator)
        {
            _appLifeTime = appLifeTime;
            _logger = logger;
            _mediator = mediator;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifeTime.ApplicationStarted.Register(() =>
            {
                Task.Run(async () =>
                {
                    try
                    {
                        // Criando um novo carrinho e adicionando itens
                        var shoppingCart = await _mediator.Send(new CreateShoppingCartCommand("Fulano de Tal"));

                        // Adicionando item ao carrinho
                        await _mediator.Send(new AdditemShoppingCartCommand(shoppingCart.Id, "Orange", 1.99));
                        await _mediator.Send(new AdditemShoppingCartCommand(shoppingCart.Id, "Strawberry", 5.99));
                        await _mediator.Send(new AdditemShoppingCartCommand(shoppingCart.Id, "Apple", 2.49));

                        // Recuperando o carrinho do repositório
                        var retrievedCart = await _mediator.Send(new GetShoppingCartQuery(shoppingCart.Id));

                        // Mostrando os dados do carrinho recuperado
                        _logger.LogInformation("============= Name =============");
                        _logger.LogInformation($"CustomerName: {retrievedCart.CustomerName}");
                        _logger.LogInformation("============= Items ============");
                        foreach (var item in retrievedCart.Items)
                        {
                            _logger.LogInformation($"ItemName: {item.ItemName} Price: {item.Price}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }
                    finally
                    {
                        _appLifeTime.StopApplication();
                    }
                });
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}