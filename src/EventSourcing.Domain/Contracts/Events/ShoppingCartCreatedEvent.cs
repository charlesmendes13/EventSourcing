using EventSourcing.Domain.Core.Common;

namespace EventSourcing.Domain.Contracts.Events
{
    public class ShoppingCartCreatedEvent : Event
    {
        public Guid ShoppingCartId { get; set; }
        public string CustomerName { get; set; }
    }
}
