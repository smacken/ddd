using DomainDriven;
using DomainDrivenSample.SalesAndCatalog.Aggregates;

namespace DomainDrivenSample.SalesAndCatalog.Entities
{
    public class Contract : Entity<long>
    {
        public Publisher Publisher { get; private set; }
        public Author Author { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateRange DateRange => new DateRange(StartDate, EndDate);
        public List<Book> BooksUnderContract { get; private set; } = new List<Book>();

        public Contract(
            long id,
            Publisher publisher,
            Author author,
            DateTime startDate,
            DateTime endDate,
            List<Book> booksUnderContract
        )
        {
            Id = id;
            Publisher = publisher;
            Author = author;
            DateRange = new DateRange(startDate, endDate);
            BooksUnderContract = booksUnderContract;
        }

        public void AddBook(Book book)
        {
            BooksUnderContract.Add(book);
        }

        public void RemoveBook(Book book)
        {
            BooksUnderContract.Remove(book);
        }

        public void ChangeDates(DateTime newStartDate, DateTime newEndDate)
        {
            StartDate = newStartDate;
            EndDate = newEndDate;
        }

        public void Renew(DateTime newEndDate)
        {
            EndDate = newEndDate;
        }

        public void Cancel()
        {
            EndDate = DateTime.Now;
        }

        public bool IsCancelled()
        {
            return EndDate < DateTime.Now;
        }

        public bool IsActive()
        {
            return StartDate <= DateTime.Now && DateTime.Now <= EndDate;
        }

        public bool IsUpcoming()
        {
            return StartDate > DateTime.Now;
        }

        public bool IsExpired()
        {
            return EndDate < DateTime.Now;
        }

        public bool IsOngoing()
        {
            return IsActive() && !IsExpired();
        }

        public void Activate()
        {
            StartDate = DateTime.Now;
        }
    }
}