using DomainDriven;
using DomainDrivenSample.SalesAndCatalog.Entities;

namespace DomainDrivenSample.SalesAndCatalog.Aggregates
{
    public class Author : AggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string Biography { get; private set; }
        public List<Book> Books { get; private set; }
        public List<Promotion> Promotions { get; private set; }

        public Author(Guid id, string name, string biography)
            : base(id)
        {
            Name = name;
            Biography = biography;
            Books = new List<Book>();
            Promotions = new List<Promotion>();
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
            Books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            Books.Remove(book);
        }

        public void AddPromotion(Promotion promotion)
        {
            Promotions.Add(promotion);
        }

        public void RemovePromotion(Guid promotionId)
        {
            var existingPromotion = Promotions.FirstOrDefault(p => p.Id == promotionId);
            if (existingPromotion == null)
            {
                throw new InvalidOperationException("Promotion not found");
            }

            Promotions.Remove(existingPromotion);
        }

        public void PublishBook(Guid bookId)
        {
            var book = Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                throw new InvalidOperationException("Book not found");
            }

            book.Publish();
        }
    }
}
