using System.Linq.Expressions;
using DomainDriven.Specification;
using DomainDrivenSample.SalesAndCatalog.Aggregates;

namespace DomainDrivenSample.SalesAndCatalog.Specification
{
    public class BookIsAvailableSpecification : Specification<Book>
    {
        public override Expression<Func<Book, bool>> SpecExpression =>
            book => book.Editions.Any(edition => edition.StockQuantity > 0);
    }

    public class BookIsByAuthorSpecification : Specification<Book>
    {
        private readonly string _authorId;

        public BookIsByAuthorSpecification(string authorId)
        {
            _authorId = authorId;
        }

        public override Expression<Func<Book, bool>> SpecExpression =>
            book => book.Metadata.Authors.Contains(_authorId);
    }

    public class BookIsUnderPriceSpecification : Specification<Book>
    {
        private readonly decimal _priceLimit;

        public BookIsUnderPriceSpecification(decimal priceLimit)
        {
            _priceLimit = priceLimit;
        }

        public override Expression<Func<Book, bool>> SpecExpression =>
            book => book.Editions.Any(edition => edition.Price.Amount <= _priceLimit);
    }

    public class BookByIsbnSpec : Specification<Book>
    {
        private readonly string _isbn;

        public BookByIsbnSpec(string isbn)
        {
            _isbn = isbn;
        }

        public override Expression<Func<Book, bool>> SpecExpression =>
            book => book.Metadata.ISBN == _isbn;
    }
}
