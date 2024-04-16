namespace DomainDriven
{
    public interface IAggregateRoot<out TId>
        where TId : notnull
    {
        public TId Id { get; }

        IReadOnlyCollection<IDomainEvent> Events { get; }

        void ClearDomainEvents();
    }

    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
        where TId : notnull
    {
        private readonly List<IDomainEvent> domainEvents = new List<IDomainEvent>();

        protected AggregateRoot()
            : base() { }

        protected AggregateRoot(TId id)
            : base(id) { }

        /// <inheritdoc/>
        public IReadOnlyCollection<IDomainEvent> Events => this.domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent eventItem)
        {
            this.domainEvents.Add(eventItem);
        }

        /// <inheritdoc/>
        public void ClearDomainEvents()
        {
            this.domainEvents.Clear();
        }
    }
}
