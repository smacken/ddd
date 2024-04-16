namespace DomainDriven
{
    public abstract class Entity<TId>
        where TId : notnull
    {
        public virtual TId Id { get; protected init; }

        protected Entity()
        {
            this.Id = default!;
        }

        protected Entity(TId id)
        {
            this.Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not Entity<TId> other)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.GetType() != other.GetType())
            {
                return false;
            }

            if (this.IsTransient() || other.IsTransient())
            {
                return false;
            }

            return this.Id.Equals(other.Id);
        }

        private bool IsTransient() => EqualityComparer<TId>.Default.Equals(this.Id, default);

        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TId> a, Entity<TId> b) => !(a == b);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(this.GetType(), this.Id);
    }

    public abstract class Entity : Entity<long>
    {
        protected Entity()
            : base()
        {
        }

        protected Entity(long id)
            : base(id)
        {
        }
    }
}
