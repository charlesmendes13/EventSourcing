namespace EventSourcing.Domain.Core.Common
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; protected set; }
        private readonly List<Event> _changes = new List<Event>();

        public void ApplyChange(Event @event)
        {
            Apply(@event);
            _changes.Add(@event);
        }        

        public IEnumerable<Event> GetChanges() => _changes.AsEnumerable();

        public void ClearChanges() => _changes.Clear();

        protected abstract void Apply(Event @event);
    }
}
