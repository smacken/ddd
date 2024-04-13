using DomainDriven;
using DomainDrivenSample.SalesAndCatalog.Entities;

namespace DomainDrivenSample.SalesAndCatalog.Aggregates
{
    public class Author : AggregateRoot<Guid>
    {
        public string ID { get; private set; }
        public string Name { get; private set; }
        public string Biography { get; private set; }
        public List<Book> Books { get; private set; }
        public List<Promotion> Promotions { get; private set; }

    }
}