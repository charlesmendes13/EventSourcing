using EventSourcing.Domain.Contracts.Events;
using EventSourcing.Domain.Core.Common;

namespace EventSourcing.Domain.AggregateModels.ShoppingCartAggregate
{
    public class ShoppingCart : AggregateRoot
    {
        public string CustomerName { get; private set; }
        public List<ItemShoppingCart> Items { get; private set; } = new List<ItemShoppingCart>();

        protected ShoppingCart() { }

        public ShoppingCart(Guid id, string customerName)
        {
            ApplyChange(new ShoppingCartCreatedEvent(id, customerName));
        }

        public void AddItem(Guid id, string itemName, double price)
        {
            ApplyChange(new ItemAddedEvent(id, itemName, price));
        }

        protected override void Apply(Event @event)
        {
            switch (@event)
            {
                case ShoppingCartCreatedEvent createdEvent:
                    Id = createdEvent.ShoppingCartId;
                    CustomerName = createdEvent.CustomerName;
                    break;
                case ItemAddedEvent addedEvent:
                    Items.Add(new ItemShoppingCart(addedEvent.ItemId,
                        addedEvent.ItemName,
                        addedEvent.Price));
                    break;
            }
        }
    }
}
