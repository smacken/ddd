namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class BookPublishedEvent : DomainEvent
    {
        public Guid BookId { get; }
        public string Title { get; }
        public Dictionary<string, object> Metadata { get; }

        public BookPublishedEvent(Guid bookId, string title, Dictionary<string, object> metadata)
        {
            BookId = bookId;
            Title = title;
            Metadata = metadata;
        }

        public override void Flatten()
        {
            AddArg("BookId", BookId);
            AddArg("Title", Title);
            AddArg("Metadata", Metadata);
        }
    }
}
