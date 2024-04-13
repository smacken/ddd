namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class BookSoldEvent
    {
        public Guid BookId { get; private set; }
        public int QuantitySold { get; private set; }

        public BookSoldEvent(Guid bookId, int quantitySold)
        {
            BookId = bookId;
            QuantitySold = quantitySold;
        }
    }
}