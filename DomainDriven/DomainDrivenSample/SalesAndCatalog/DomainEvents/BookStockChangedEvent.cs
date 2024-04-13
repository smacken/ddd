using DomainDriven;

namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class BookStockChangedEvent : DomainEvent
    {
        public Guid BookId { get; private set; }
        public int NewStockLevel { get; private set; }

        public BookStockChangedEvent(Dictionary<string, object> args, string? correlationId = null, int version = 1) 
            : base(args, correlationId, version)
        {
        }
    }
}