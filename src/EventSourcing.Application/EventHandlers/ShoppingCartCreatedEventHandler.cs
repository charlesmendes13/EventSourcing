using EventSourcing.Domain.Contracts.Events;
using MediatR;

namespace EventSourcing.Application.EventHandlers
{
    public class ShoppingCartCreatedEventHandler 
        : INotificationHandler<ShoppingCartCreatedEvent>
    {
        public ShoppingCartCreatedEventHandler() { }

        public Task Handle(ShoppingCartCreatedEvent @event, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
