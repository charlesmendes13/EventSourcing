using EventSourcing.Domain.Core.Common;

namespace EventSourcing.Infrastructure.Data.EventStores
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly Dictionary<Guid, List<Event>> _store = new Dictionary<Guid, List<Event>>();       

        public IEnumerable<Event> GetEvents(Guid aggregateId)
        {
            if (_store.TryGetValue(aggregateId, out var events))
            {
                return events;
            }

            return new List<Event>();
        }

        public void SaveEvents(Guid aggregateId, IEnumerable<Event> events)
        {
            if (!_store.ContainsKey(aggregateId))
            {
                _store.Add(aggregateId, new List<Event>());
            }

            _store[aggregateId].AddRange(events);
        }
    }   
}
