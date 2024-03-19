using EventSourcing.Domain.Core.Common;

namespace EventSourcing.Domain.AggregateModels.ShoppingCartAggregate
{
    public interface IShoppingCartRepository 
        : IRepository<ShoppingCart>
    {
    }
}
