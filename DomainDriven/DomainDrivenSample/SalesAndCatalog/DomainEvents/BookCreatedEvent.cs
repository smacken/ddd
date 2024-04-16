namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class BookCreatedEvent : DomainEvent
    {
        public Guid BookId { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }

        public BookCreatedEvent(Guid bookId, string title, string author)
            : base()
        {
            BookId = bookId;
            Title = title;
            Author = author;
        }

        public override void Flatten()
        {
            AddArg("BookId", BookId);
            AddArg("Title", Title);
            AddArg("Author", Author);
        }
    }
}
