using DomainDrivenSample.SalesAndCatalog.Aggregates;

namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class BookUpdatedEvent : DomainEvent
    {
        public Guid BookId { get; private set; }
        public string UpdatedProperty { get; private set; }
        public Book NewValue { get; private set; }

        public BookUpdatedEvent(Guid bookId, string updatedProperty, Book newValue)
            : base()
        {
            BookId = bookId;
            UpdatedProperty = updatedProperty;
            NewValue = newValue;
        }

        public override void Flatten()
        {
            AddArg("BookId", BookId);
            AddArg("UpdatedProperty", UpdatedProperty);
            AddArg("NewValue", NewValue);
        }
    }
}
