using DomainDrivenSample.SalesAndCatalog.Aggregates;

namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class BookSubmittedEvent : DomainEvent
    {
        public Book Book { get; private set; }
        public Author Author { get; private set; }

        public BookSubmittedEvent(Author author, Book book)
            : base()
        {
            Author = author;
            Book = book;
        }

        public override void Flatten()
        {
            AddArg("BookId", Book.Metadata.ISBN.Value);
            AddArg("Title", Book.Metadata.Title);
            AddArg("Authors", string.Join(',', Book.Metadata.Authors));
        }
    }
}
