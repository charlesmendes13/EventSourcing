using EventSourcing.Domain.AggregateModels.ShoppingCartAggregate;
using MediatR;

namespace EventSourcing.Domain.Contracts.Queries
{
    public class GetShoppingCartQuery : IRequest<ShoppingCart>
    {
        public Guid ShoppingCartId { get; set; }

        public GetShoppingCartQuery(Guid shoppingCartId) 
        {
            ShoppingCartId = shoppingCartId;
        }
    }
}
