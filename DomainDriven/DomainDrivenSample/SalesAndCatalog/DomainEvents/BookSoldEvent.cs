using DomainDriven;

namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class BookSoldEvent : DomainEvent
    {
        public Guid BookId { get; private set; }
        public int QuantitySold { get; private set; }

        public BookSoldEvent(Dictionary<string, object> args, string? correlationId = null, int version = 1) 
            : base(args, correlationId, version)
        {
        }
    }
}