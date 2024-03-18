namespace EventSourcing.Domain.Core.Common
{
    public interface IRepository<T> where T : AggregateRoot
    {
        T GetById(Guid id);
        void Save(T aggregate);
    }
}
