namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class BookUpdatedEvent
    {
        public Guid BookId { get; private set; }
        public string UpdatedProperty { get; private set; }
        public object NewValue { get; private set; }

        public BookUpdatedEvent(Guid bookId, string updatedProperty, object newValue)
        {
            BookId = bookId;
            UpdatedProperty = updatedProperty;
            NewValue = newValue;
        }
    }
}