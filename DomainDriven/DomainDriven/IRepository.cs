namespace DomainDriven
{
    using DomainDriven.Specification;

    public interface IRepository<TEntity, in TId>
        where TEntity : IAggregateRoot<TId>
        where TId : notnull
    {
        TEntity? FindById(TId id);

        TEntity? FindOne(ISpecification<TEntity> spec);

        IEnumerable<TEntity> Find(ISpecification<TEntity> spec);

        TEntity? GetById(TId id);

        IEnumerable<TEntity> GetAll();

        Task Add(TEntity entity, CancellationToken cancellationToken = default);

        void Remove(TEntity entity);

        void SaveChanges();
    }

    public interface IRepository<TEntity> : IRepository<TEntity, Guid>
        where TEntity : IAggregateRoot<Guid> { }
}
