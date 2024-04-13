namespace DomainDriven
{
    public abstract class Entity<TId> where TId : notnull
    {
        public virtual TId Id { get; protected init; }

        protected Entity()
        {
            Id = default!;
        }

        protected Entity(TId id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public override bool Equals(object obj)
        {
            if (obj is not Entity<TId> other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (IsTransient() || other.IsTransient())
                return false;

            return Id.Equals(other.Id);
        }

        private bool IsTransient()
        {
            return EqualityComparer<TId>.Default.Equals(Id, default);
        }

        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TId> a, Entity<TId> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GetType(), Id);
        }
    }

    public abstract class Entity : Entity<long>
    {
        protected Entity() : base()
        {
        }

        protected Entity(long id) : base(id)
        {
        }
    }
}