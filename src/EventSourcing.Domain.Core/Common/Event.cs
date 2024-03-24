namespace EventSourcing.Domain.Core.Common
{
    public abstract class Event
    {
        public Guid Id { get; protected set; }
        public DateTime Timestamp { get; }

        protected Event(Guid id)
        {
            Id = id;
            Timestamp = DateTime.Now;
        }
    }
}
