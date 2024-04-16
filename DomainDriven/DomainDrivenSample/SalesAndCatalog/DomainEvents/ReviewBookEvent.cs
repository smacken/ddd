using DomainDrivenSample.SalesAndCatalog.Aggregates;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class ReviewBookEvent : DomainEvent
    {
        public Reader Reader { get; }
        public Book Book { get; }
        public string Content { get; }
        public Rating Rating { get; }

        public ReviewBookEvent(Reader reader, Book book, string content, Rating rating)
        {
            Reader = reader;
            Book = book;
            Content = content;
            Rating = rating;
        }

        public override void Flatten()
        {
            AddArg("Reader", Reader);
            AddArg("Book", Book);
            AddArg("Content", Content);
            AddArg("Rating", Rating);
        }
    }
}
