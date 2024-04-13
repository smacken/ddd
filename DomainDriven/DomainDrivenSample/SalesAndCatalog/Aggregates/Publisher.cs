using DomainDriven;
using DomainDrivenSample.SalesAndCatalog.Entities;

namespace DomainDrivenSample.SalesAndCatalog.Aggregates
{
    public class Publisher : AggregateRoot<Guid>
    {
        public string ID { get; private set; }
        public string Name { get; private set; }
        public List<Contract> Contracts { get; private set; }

        // Constructor, methods, etc.
    }
}