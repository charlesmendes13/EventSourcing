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
                        var shoppingCart = await _mediator.Send(new CreateShoppingCartCommand("Fulano de Tal",
                            new List<string>
                            {
                                "Apple",
                                "Orange",
                                "Strawberry"
                            }));

                        // Recuperando o carrinho do repositório
                        var retrievedCart = await _mediator.Send(new GetShoppingCartQuery(shoppingCart.Id));

                        // Verificando se o carrinho foi recuperado corretamente
                        _logger.LogInformation($"Customer: {retrievedCart.CustomerName}");
                        _logger.LogInformation("==== Items ====");

                        foreach (var item in retrievedCart.Items)
                        {
                            _logger.LogInformation($"ItemName: {item}");
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