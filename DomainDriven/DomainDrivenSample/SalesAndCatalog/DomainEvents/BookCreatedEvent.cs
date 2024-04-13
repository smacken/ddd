using DomainDriven;

namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class BookCreatedEvent : DomainEvent
    {
        public Guid BookId { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }

        public BookCreatedEvent(
            Guid bookId,
            string title,
            string author,
            string? correlationId = null,
            int version = 1
        )
            : base(
                new Dictionary<string, object>()
                {
                    { "BookId", bookId.ToString() },
                    { "Title", title },
                    { "Author", author }
                },
                correlationId,
                version
            )
        {
            BookId = bookId;
            Title = title;
            Author = author;
            CorrelationID = correlationId;
        }
    }
}
