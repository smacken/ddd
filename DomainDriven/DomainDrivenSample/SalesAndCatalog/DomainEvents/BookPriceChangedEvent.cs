using DomainDriven;

namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class BookPriceChangedEvent : DomainEvent
    {
        public Guid BookId { get; private set; }
        public decimal NewPrice { get; private set; }

        public BookPriceChangedEvent(Dictionary<string, object> args, string? correlationId = null, int version = 1) 
            : base(args, correlationId, version)
        {
        }
    }
}