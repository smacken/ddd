namespace DomainDrivenSample.SalesAndCatalog.Specification
{
    using System.Linq.Expressions;
    using DomainDriven.Specification;
    using DomainDrivenSample.SalesAndCatalog.Aggregates;

    public class OfferablePublishesSpec : Specification<Publisher>
    {
        public override Expression<Func<Publisher, bool>> SpecExpression =>
            p => p.Status == PublisherStatus.OpenToOffers;
    }
}
