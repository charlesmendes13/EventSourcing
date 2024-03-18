using EventSourcing.Domain.Contracts.Events;
using EventSourcing.Domain.Core.Common;

namespace EventSourcing.Domain.AggregateModels.ShoppingCartAggregate
{
    public class ShoppingCart : AggregateRoot
    {
        public string CustomerName { get; private set; }
        public List<string> Items { get; private set; } = new List<string>();

        public ShoppingCart() { }

        public ShoppingCart(Guid id, string customerName)
        {
            ApplyChange(new ShoppingCartCreatedEvent(id, customerName));
        }

        public void AddItem(Guid id, string itemName)
        {
            ApplyChange(new ItemAddedEvent(id, itemName));
        }

        protected override void Apply(Event @event)
        {
            switch (@event)
            {
                case ShoppingCartCreatedEvent createdEvent:
                    Id = createdEvent.Id;
                    CustomerName = createdEvent.CustomerName;
                    break;
                case ItemAddedEvent addedEvent:
                    Items.Add(addedEvent.ItemName);
                    break;
            }
        }
    }
}
