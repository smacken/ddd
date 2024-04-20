namespace DomainDriven
{
    public interface IUnitOfWork : IDisposable
    {
        // Commits all changes made within the unit of work.
        Task CommitAsync(CancellationToken cancellationToken = default);

        // Rolls back changes made within the unit of work.
        void Rollback();

        // Retrieves a repository for the type TEntity.
        IRepository<TEntity> Repository<TEntity>()
            where TEntity : IAggregateRoot<Guid>;
    }
}
