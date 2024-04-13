namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class BookStockChangedEvent
    {
        public Guid BookId { get; private set; }
        public int NewStockLevel { get; private set; }

        public BookStockChangedEvent(Guid bookId, int newStockLevel)
        {
            BookId = bookId;
            NewStockLevel = newStockLevel;
        }
    }
}