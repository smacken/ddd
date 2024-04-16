using System.Collections.ObjectModel;
using DomainDrivenSample.SalesAndCatalog.DomainEvents;
using DomainDrivenSample.SalesAndCatalog.Entities;

namespace DomainDrivenSample.SalesAndCatalog.Aggregates
{
    public class Author : AggregateRoot<Guid>
    {
        private List<Book> _books = new();
        private List<Promotion> _promotions = new();

        public string Name { get; private set; }
        public string Biography { get; private set; }
        public ReadOnlyCollection<Book> Books => _books.AsReadOnly();
        public ReadOnlyCollection<Promotion> Promotions => _promotions.AsReadOnly();

        public Author(Guid id, string name, string biography)
            : base(id)
        {
            Name = name;
            Biography = biography;
            _books = new List<Book>();
            _promotions = new List<Promotion>();
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateBiography(string biography)
        {
            Biography = biography;
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            _books.Remove(book);
        }

        public void AddPromotion(Promotion promotion)
        {
            _promotions.Add(promotion);
        }

        public void RemovePromotion(Guid promotionId)
        {
            Promotion? existingPromotion =
                Promotions.FirstOrDefault(p => p.Id == promotionId)
                ?? throw new InvalidOperationException("Promotion not found");
            _promotions.Remove(existingPromotion);
        }

        public void PublishBook(Guid bookId)
        {
            Book? book =
                Books.FirstOrDefault(b => b.Id == bookId)
                ?? throw new InvalidOperationException("Book not found");
            book.Publish();

            DomainDriven.DomainEvents.Raise(
                new BookPublishedEvent(book.Id, book.Metadata.Title, book.Metadata.ToDictionary())
            );
        }
    }
}
