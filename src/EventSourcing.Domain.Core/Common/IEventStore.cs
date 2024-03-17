namespace EventSourcing.Domain.Core.Common
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, IEnumerable<Event> events);
        IEnumerable<Event> GetEvents(Guid aggregateId);
    }
}
