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

    public class BookIsByAuthorSpecification(string authorId) : Specification<Book>
    {
        private readonly string _authorId = authorId;

        public override Expression<Func<Book, bool>> SpecExpression => book =>
            book.Metadata.Authors.Contains(_authorId);
    }

    public class BookIsUnderPriceSpecification(decimal priceLimit) : Specification<Book>
    {
        private readonly decimal _priceLimit = priceLimit;

        public override Expression<Func<Book, bool>> SpecExpression => book =>
            book.Editions.Any(edition => edition.Price.Amount <= _priceLimit);
    }
}
