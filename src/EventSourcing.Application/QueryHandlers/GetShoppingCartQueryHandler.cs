using EventSourcing.Domain.AggregateModels.ShoppingCartAggregate;
using EventSourcing.Domain.Contracts.Queries;
using EventSourcing.Domain.Core.Common;
using MediatR;

namespace EventSourcing.Application.QueryHandlers
{
    public class GetShoppingCartQueryHandler : IRequestHandler<GetShoppingCartQuery, ShoppingCart>
    {
        private readonly IRepository<ShoppingCart> _repository;

        public GetShoppingCartQueryHandler(IRepository<ShoppingCart> repository) 
        {
            _repository = repository;
        }

        public Task<ShoppingCart> Handle(GetShoppingCartQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = _repository.GetById(request.ShoppingCartId);

            if (shoppingCart == null)
                throw new KeyNotFoundException(nameof(shoppingCart));

            return Task.FromResult(shoppingCart);
        }
    }
}
