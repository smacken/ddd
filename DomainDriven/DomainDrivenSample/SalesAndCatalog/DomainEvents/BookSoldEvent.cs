namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class BookSoldEvent : DomainEvent
    {
        public Guid BookId { get; private set; }
        public int QuantitySold { get; private set; }

        public BookSoldEvent(Guid bookId, int quantitySold)
            : base()
        {
            BookId = bookId;
            QuantitySold = quantitySold;
        }

        public override void Flatten()
        {
            AddArg("BookId", BookId);
            AddArg("QuantitySold", QuantitySold);
        }
    }
}
