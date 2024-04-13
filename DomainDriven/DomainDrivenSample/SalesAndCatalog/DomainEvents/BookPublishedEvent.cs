namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class BookPublishedEvent : DomainEvent
    {
        public Guid BookId { get; }
        public string Title { get; }
        public Dictionary<string, object> Metadata { get; }

        public BookPublishedEvent(Guid bookId, string title, Dictionary<string, object> metadata)
            : base(
                new Dictionary<string, object>()
                {
                    { "BookId", bookId.ToString() },
                    { "Title", title },
                    { "Metadata", metadata }
                }
            )
        {
            BookId = bookId;
            Title = title;
            Metadata = metadata;
        }
    }
}
