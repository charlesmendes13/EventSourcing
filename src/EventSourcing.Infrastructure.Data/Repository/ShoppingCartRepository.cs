using EventSourcing.Domain.AggregateModels.ShoppingCartAggregate;
using EventSourcing.Domain.Core.Common;

namespace EventSourcing.Infrastructure.Data.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IEventStore _eventStore;

        public ShoppingCartRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public ShoppingCart GetById(Guid id)
        {
            var events = _eventStore.GetEvents(id);
            var shoppingCart = Activator.CreateInstance(typeof(ShoppingCart), nonPublic: true) as ShoppingCart;

            shoppingCart.LoadsFromHistory(events);

            return shoppingCart;
        }

        public void Save(ShoppingCart shoppingCart)
        {
            _eventStore.SaveEvents(shoppingCart.Id, shoppingCart.GetChanges());
            shoppingCart.ClearChanges();
        }
    }
}
