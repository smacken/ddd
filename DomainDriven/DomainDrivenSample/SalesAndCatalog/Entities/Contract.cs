using System.Collections.ObjectModel;
using DomainDrivenSample.SalesAndCatalog.Aggregates;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.Entities
{
    public class Contract : Entity<long>
    {
        private List<Book> _booksUnderContract = new List<Book>();
        public Publisher Publisher { get; private set; }
        public Author Author { get; private set; }
        public DateRange DateRange { get; private set; }
        public ReadOnlyCollection<Book> BooksUnderContract => _booksUnderContract.AsReadOnly();

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
            _booksUnderContract = booksUnderContract;
        }

        public void ChangeDates(DateTime newStartDate, DateTime newEndDate)
        {
            DateRange = new DateRange(newStartDate, newEndDate);
        }
    }
}
