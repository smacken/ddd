namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class BookStockChangedEvent : DomainEvent
    {
        public BookStockChangedEvent(Guid bookId, int adjustment)
        {
            BookId = bookId;
            Adjustment = adjustment;
        }

        public override void Flatten()
        {
            AddArg("BookId", BookId);
            AddArg("NewStockLevel", Adjustment);
        }

        public Guid BookId { get; private set; }
        public int Adjustment { get; private set; }
    }
}
