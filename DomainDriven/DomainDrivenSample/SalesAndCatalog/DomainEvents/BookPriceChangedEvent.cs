namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class BookPriceChangedEvent
    {
        public Guid BookId { get; private set; }
        public decimal NewPrice { get; private set; }

        public BookPriceChangedEvent(Guid bookId, decimal newPrice)
        {
            BookId = bookId;
            NewPrice = newPrice;
        }
    }
}