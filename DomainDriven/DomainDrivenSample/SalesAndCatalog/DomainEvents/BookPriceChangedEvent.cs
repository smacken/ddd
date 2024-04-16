namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class BookPriceChangedEvent : DomainEvent
    {
        public Guid BookId { get; private set; }
        public decimal NewPrice { get; private set; }

        public BookPriceChangedEvent(Guid bookId, decimal newPrice)
            : base()
        {
            BookId = bookId;
            NewPrice = newPrice;
        }

        public override void Flatten()
        {
            AddArg("BookId", BookId);
            AddArg("NewPrice", NewPrice);
        }
    }
}
