using System.Reflection;
using NRules;
using NRules.Fluent;

namespace DomainDriven.Rules;

public abstract class RulesEngineUnitOfWork : IUnitOfWork
{
    private readonly ISession _session;
    private bool _disposed;

    protected RulesEngineUnitOfWork()
    {
        var repository = new RuleRepository();
        repository.Load(x => x.From(Assembly.GetExecutingAssembly()));

        var factory = repository.Compile();
        _session = factory.CreateSession();
    }

    public ISession GetSession()
    {
        return _session;
    }

    public virtual Task CommitAsync(CancellationToken cancellationToken = default)
    {
        _session.Fire();
        return Task.CompletedTask;
    }

    public abstract void Rollback();

    public IRepository<TEntity> Repository<TEntity>()
        where TEntity : IAggregateRoot<Guid>
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _disposed = true;
        }
    }
}
