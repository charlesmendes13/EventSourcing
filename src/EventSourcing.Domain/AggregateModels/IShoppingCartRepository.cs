namespace EventSourcing.Domain.AggregateModels
{
    public interface IShoppingCartRepository
    {
        ShoppingCart GetById(Guid id);
        void Save(ShoppingCart shoppingCart);
    }
}
