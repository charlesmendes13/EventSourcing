namespace EventSourcing.Domain.Core.Common
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; protected set; }
        private readonly List<Event> _changes = new List<Event>();

        protected void ApplyChange(Event @event) => ApplyChange(@event, true);

        private void ApplyChange(Event @event, bool isNew)
        {
            Apply(@event);

            if (isNew)
                _changes.Add(@event);
        }

        protected abstract void Apply(Event @event);

        public void LoadsFromHistory(IEnumerable<Event> history)
        {
            foreach (var @event in history)
            {
                ApplyChange(@event, false);
            }
        }

        public IEnumerable<Event> GetChanges() => _changes.AsEnumerable();

        public void ClearChanges() => _changes.Clear();  
    }
}
