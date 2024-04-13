using System.Collections.ObjectModel;

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
        public string Type => GetType().Name;
        public DateTime Created { get; }
        public IReadOnlyDictionary<string, object> Args { get; }
        public string? CorrelationID { get; set; }
        public int Version { get; }

        protected DomainEvent(Dictionary<string, object> args, string? correlationId = null, int version = 1)
        {
            Created = DateTime.UtcNow;
            Args = new ReadOnlyDictionary<string, object>(args);
            CorrelationID = correlationId ?? Guid.NewGuid().ToString();
            Version = version;
        }
    }

    public class DomainEventRecord
    {
        public string Type { get; set; }
        public List<KeyValuePair<string, string>> Args { get; set; }
        public string? CorrelationID { get; set; }
        public DateTime Created { get; set; }

        public DomainEventRecord(string type, List<KeyValuePair<string, string>> args, string? correlationID)
        {
            Type = type;
            Args = args ?? new List<KeyValuePair<string, string>>();
            CorrelationID = correlationID;
            Created = DateTime.UtcNow;
        }
    }

    public interface IRequestCorrelationIdentifier
    {
        string? CorrelationId { get; }
    }

    public interface Handles<T> where T : DomainEvent
    {
        Task Handle(T args);
    }

    public class DomainEventHandle<TDomainEvent> : Handles<TDomainEvent>
        where TDomainEvent : DomainEvent
    {
        private readonly IRequestCorrelationIdentifier _requestCorrelationIdentifier;

        public DomainEventHandle(IRequestCorrelationIdentifier requestCorrelationIdentifier)
        {
            _requestCorrelationIdentifier = requestCorrelationIdentifier ?? throw new ArgumentNullException(nameof(requestCorrelationIdentifier));
        }

        public virtual Task Handle(TDomainEvent @event)
        {
            if (@event is null)
                throw new ArgumentNullException(nameof(@event));

            @event.CorrelationID = _requestCorrelationIdentifier.CorrelationId; 
            return Task.CompletedTask;
        }
    }
}