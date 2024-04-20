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
    }

    public interface IRepository<TEntity> : IRepository<TEntity, Guid>
        where TEntity : IAggregateRoot<Guid>
    { }

    public interface IRepositoryFactory
    {
        IRepository<T> CreateRepository<T>()
            where T : IAggregateRoot<Guid>;
        IRepository<T, TId> CreateRepository<T, TId>()
            where T : IAggregateRoot<TId>
            where TId : notnull;
    }
}
