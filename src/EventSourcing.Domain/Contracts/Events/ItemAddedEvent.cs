using EventSourcing.Domain.Core.Common;
using MediatR;

namespace EventSourcing.Domain.Contracts.Events
{
    public class ItemAddedEvent 
        : Event, INotification
    {
        public Guid ItemId { get; private set; }
        public string ItemName { get; private set; }
        public double Price { get; private set; }

        public ItemAddedEvent(Guid id, string itemName, double price)
            : base (id)
        {
            ItemId = id;
            ItemName = itemName;
            Price = price;
        }
    }
}
