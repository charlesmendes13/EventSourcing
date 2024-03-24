using EventSourcing.Domain.AggregateModels.ShoppingCartAggregate;
using EventSourcing.Domain.Contracts.Commands;
using MediatR;

namespace EventSourcing.Application.CommandHandlers
{
	public class AdditemShoppingCartCommandHandler : IRequestHandler<AdditemShoppingCartCommand>
	{
        private readonly IShoppingCartRepository _repository;

        public AdditemShoppingCartCommandHandler(IShoppingCartRepository repository)
		{
            _repository = repository;
        }

        public Task Handle(AdditemShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = _repository.GetById(request.ShoppingCartId);

            if (shoppingCart == null)
                throw new KeyNotFoundException(nameof(shoppingCart));

            shoppingCart.AddItem(Guid.NewGuid(), request.ItemName, request.Price);

            _repository.Save(shoppingCart);

            return Task.CompletedTask;
        }
    }
}

