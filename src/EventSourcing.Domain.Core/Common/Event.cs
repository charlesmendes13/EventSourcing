namespace EventSourcing.Domain.Core.Common
{
    public abstract class Event
    {
        public Guid Id { get; } 
        public DateTime Timestamp { get; }

        public Event() 
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
        }
    }
}
