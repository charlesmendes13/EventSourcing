using EventSourcing.Domain.AggregateModels.ShoppingCartAggregate;
using EventSourcing.Domain.Contracts.Commands;
using EventSourcing.Domain.Core.Common;
using MediatR;

namespace EventSourcing.Application.CommandHandlers
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCart>
    {
        private readonly IRepository<ShoppingCart> _repository;

        public CreateShoppingCartCommandHandler(IRepository<ShoppingCart> repository)
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
