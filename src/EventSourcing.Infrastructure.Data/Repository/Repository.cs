using EventSourcing.Domain.Core.Common;

namespace EventSourcing.Infrastructure.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot, new()
    {
        private readonly IEventStore _eventStore;

        public Repository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public T GetById(Guid id)
        {
            var events = _eventStore.GetEvents(id);
            var aggregate = new T();

            foreach (var @event in events)
            {
                aggregate.ApplyChange(@event);
            }

            return aggregate;
        }

        public void Save(T aggregate)
        {
            _eventStore.SaveEvents(aggregate.Id, aggregate.GetChanges());
            aggregate.ClearChanges();
        }
    }
}
