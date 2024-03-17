using EventSourcing.Domain.Contracts.Events;
using MediatR;

namespace EventSourcing.Application.EventHandlers
{
    public class ItemAddedEventHandler 
        : INotificationHandler<ItemAddedEvent>
    {
        public ItemAddedEventHandler() { }

        public Task Handle(ItemAddedEvent @event, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
