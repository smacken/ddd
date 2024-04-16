namespace DomainDriven
{
    public interface IDomainEvent
    {
        string Type { get; }

        DateTime Created { get; }

        IReadOnlyDictionary<string, object> Args { get; }

        string? CorrelationID { get; }

        int Version { get; }
    }

    public abstract class DomainEvent : IDomainEvent
    {
        private readonly Dictionary<string, object> _args;

        /// <inheritdoc/>
        public string Type => this.GetType().Name;

        /// <inheritdoc/>
        public DateTime Created { get; } = DateTime.UtcNow;

        /// <inheritdoc/>
        public IReadOnlyDictionary<string, object> Args => _args.AsReadOnly();

        /// <inheritdoc/>
        public string? CorrelationID { get; set; }

        /// <inheritdoc/>
        public int Version { get; }

        public DomainEvent()
        {
            _args = new Dictionary<string, object>();
        }

        public abstract void Flatten();

        protected void AddArg(string key, object value) => _args.Add(key, value);
    }

    public class DomainEventRecord
    {
        public string Type { get; set; }

        public List<KeyValuePair<string, string>> Args { get; set; }

        public string? CorrelationID { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DomainEventRecord(
            string type,
            List<KeyValuePair<string, string>> args,
            string? correlationID = null
        )
        {
            this.Type = type;
            this.Args = args;
            this.CorrelationID = correlationID;
        }
    }

    public interface IRequestCorrelationIdentifier
    {
        string? CorrelationId { get; }
    }

    public interface Handles<T>
        where T : DomainEvent
    {
        Task Handle(T args, CancellationToken cancellationToken = default);
    }

    public class DomainEventHandle<TDomainEvent> : Handles<TDomainEvent>
        where TDomainEvent : DomainEvent
    {
        private readonly IRequestCorrelationIdentifier requestCorrelationIdentifier;

        public DomainEventHandle(IRequestCorrelationIdentifier requestCorrelationIdentifier)
        {
            this.requestCorrelationIdentifier = requestCorrelationIdentifier;
        }

        public Task Handle(TDomainEvent @event, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(@event);
            @event.Flatten();
            @event.CorrelationID = this.requestCorrelationIdentifier.CorrelationId;
            return Task.CompletedTask;
        }
    }
}
