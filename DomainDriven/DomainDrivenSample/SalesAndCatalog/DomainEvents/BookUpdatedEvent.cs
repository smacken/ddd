using DomainDrivenSample.SalesAndCatalog.Aggregates;

namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class BookUpdatedEvent : DomainEvent
    {
        public Guid BookId { get; private set; }
        public string UpdatedProperty { get; private set; }
        public Book NewValue { get; private set; }

        public BookUpdatedEvent(Guid bookId, string updatedProperty, Book newValue)
            : base(
                new Dictionary<string, object>
                {
                    { "BookId", bookId.ToString() },
                    { "UpdatedProperty", updatedProperty },
                    { "NewValue", newValue.ToString() }
                }
            )
        {
            BookId = bookId;
            UpdatedProperty = updatedProperty;
            NewValue = newValue;
        }
    }
}
