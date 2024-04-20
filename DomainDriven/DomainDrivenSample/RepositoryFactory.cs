namespace DomainDrivenSample;

using Microsoft.Extensions.DependencyInjection;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;

    public RepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider =
            serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public IRepository<T> CreateRepository<T>()
        where T : IAggregateRoot<Guid>
    {
        return _serviceProvider.GetRequiredService<IRepository<T>>();
    }

    public IRepository<T, TId> CreateRepository<T, TId>()
        where T : IAggregateRoot<TId>
        where TId : notnull
    {
        return _serviceProvider.GetRequiredService<IRepository<T, TId>>();
    }
}
