namespace DomainDrivenSample;

public class InMemoryUnitOfWork : IUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = new();
    private readonly IRepositoryFactory _repositoryFactory;
    private bool _disposed;

    public InMemoryUnitOfWork(IRepositoryFactory repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
    }

    public IRepository<TEntity> Repository<TEntity>()
        where TEntity : IAggregateRoot<Guid>
    {
        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = _repositoryFactory.CreateRepository<TEntity>();
        }

        return (IRepository<TEntity>)_repositories[type];
    }

    public Task CommitAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public void Rollback() { }

    public void Dispose()
    {
        if (!_disposed)
        {
            _repositories.Clear();
            _disposed = true;
        }
    }
}
