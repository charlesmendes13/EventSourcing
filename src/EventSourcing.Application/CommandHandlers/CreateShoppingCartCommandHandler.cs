using EventSourcing.Domain.AggregateModels.ShoppingCartAggregate;
using EventSourcing.Domain.Contracts.Commands;
using MediatR;

namespace EventSourcing.Application.CommandHandlers
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCart>
    {
        private readonly IShoppingCartRepository _repository;

        public CreateShoppingCartCommandHandler(IShoppingCartRepository repository)
        {
            _repository = repository;
        }

        public Task<ShoppingCart> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = new ShoppingCart(Guid.NewGuid(), request.CustomerName);
            
            foreach (var item in request.Items)
                shoppingCart.AddItem(Guid.NewGuid(), item);

            _repository.Save(shoppingCart);

            return Task.FromResult(shoppingCart);
        }
    }
}
