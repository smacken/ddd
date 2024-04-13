using DomainDriven;
using DomainDrivenSample.SalesAndCatalog.Entities;

namespace DomainDrivenSample.SalesAndCatalog.Aggregates
{
    public class Reader : AggregateRoot<Guid>
    {
        public string ID { get; private set; }
        public string Name { get; private set; }
        public List<Subscription> Subscriptions { get; private set; }
        public List<Review> Reviews { get; private set; }

        // Constructor, methods, etc.
    }
}