using EventSourcing.Domain.Core.Common;

namespace EventSourcing.Domain.Contracts.Events
{
    public class ItemAddedEvent : Event
    {
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
    }
}
