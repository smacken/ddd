namespace DomainDrivenSample;

using DomainDriven;
using DomainDriven.Specification;

public class InMemoryRepository<TEntity> : IRepository<TEntity, Guid>
    where TEntity : IAggregateRoot<Guid>
{
    private readonly Dictionary<Guid, TEntity> _entities = new ();

    public TEntity? FindById(Guid id)
    {
        _entities.TryGetValue(id, out TEntity? entity);
        return entity;
    }

    public TEntity? FindOne(ISpecification<TEntity> spec)
    {
        return _entities.Values.FirstOrDefault(spec.IsSatisfiedBy);
    }

    public IEnumerable<TEntity> Find(ISpecification<TEntity> spec)
    {
        return _entities.Values.Where(spec.IsSatisfiedBy);
    }

    public TEntity? GetById(Guid id)
    {
        if (!_entities.TryGetValue(id, out TEntity? entity))
        {
            throw new KeyNotFoundException("Entity with the specified ID was not found.");
        }
        return entity;
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _entities.Values;
    }

    public void Add(TEntity entity)
    {
        _entities[entity.Id] = entity;
    }

    public void Remove(TEntity entity)
    {
        _entities.Remove(entity.Id);
    }

    public void SaveChanges()
    {
        // In a real repository, this would commit changes to the data store.
        // Since this is an in-memory repository, changes are already "saved" when Add or Remove is called.
        // This method can be left empty or used to raise domain events if needed.
    }
}
