using System.Collections.ObjectModel;
using DomainDrivenSample.SalesAndCatalog.Aggregates;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.Entities
{
    public class Contract : Entity<Guid>
    {
        private readonly List<Book> _booksUnderContract = new();
        private ContractType _contractType;

        public Publisher Publisher { get; private set; }
        public Author Author { get; private set; }
        public DateRange DateRange { get; private set; }
        public string? Royalties { get; private set; }
        public string? Deadlines { get; private set; }
        public string? LegalTerms { get; private set; }
        public decimal? Advances { get; private set; }
        public ReadOnlyCollection<Book> BooksUnderContract => _booksUnderContract.AsReadOnly();

        public Contract(
            Guid id,
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

        public Contract(Author author, Publisher publisher, Book book)
        {
            Id = Guid.NewGuid();
            Author = author;
            Publisher = publisher;
            _booksUnderContract.Add(book);
            DateRange = new DateRange(DateTime.Now, DateTime.Now.AddYears(1));
        }

        public Contract(Author author, Publisher publisher, Book book, ContractType contractType)
            : this(author, publisher, book)
        {
            Id = Guid.NewGuid();
            Author = author;
            Publisher = publisher;
            DateRange = new DateRange(DateTime.Now, DateTime.Now.AddDays(1));
            _booksUnderContract.Add(book);
            _contractType = contractType;
        }

        public void SetContractDetails(string? royalties, string? deadlines, string? terms)
        {
            if (royalties != null)
                Royalties = royalties;
            if (deadlines != null)
                Deadlines = deadlines;
            if (terms != null)
                LegalTerms = terms;
        }

        public void ChangeDates(DateTime newStartDate, DateTime newEndDate)
        {
            DateRange = new DateRange(newStartDate, newEndDate);
        }

        internal void SetAdvances(decimal advance)
        {
            if (advance < 0)
                throw new ArgumentException(nameof(advance));
            Advances = advance;
        }
    }
}
