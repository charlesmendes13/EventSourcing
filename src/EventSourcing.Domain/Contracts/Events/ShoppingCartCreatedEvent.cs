using EventSourcing.Domain.Core.Common;
using MediatR;

namespace EventSourcing.Domain.Contracts.Events
{
    public class ShoppingCartCreatedEvent 
        : Event, INotification
    {
        public Guid ShoppingCartId { get; private set; }
        public string CustomerName { get; private set; }

        public ShoppingCartCreatedEvent(Guid id, string customerName)
        {
            ShoppingCartId = id;
            CustomerName = customerName;
        }
    }
}
