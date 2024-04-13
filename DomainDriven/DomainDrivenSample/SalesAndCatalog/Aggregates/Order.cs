using DomainDriven;
using DomainDrivenSample.SalesAndCatalog.Entities;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.Aggregates
{
    public class Order : AggregateRoot<Guid>
    {
        public string ID { get; private set; }
        public Reader Buyer { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }
        public DateTime DatePlaced { get; private set; }
        public OrderStatus Status { get; private set; }

        // Constructor, methods, etc.
    }
}