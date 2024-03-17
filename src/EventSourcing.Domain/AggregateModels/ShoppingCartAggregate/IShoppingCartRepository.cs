namespace EventSourcing.Domain.AggregateModels.ShoppingCartAggregate
{
    public interface IShoppingCartRepository
    {
        ShoppingCart GetById(Guid id);
        void Save(ShoppingCart shoppingCart);
    }
}
