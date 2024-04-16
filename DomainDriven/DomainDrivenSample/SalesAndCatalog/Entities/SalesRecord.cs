using DomainDriven;
using DomainDrivenSample.SalesAndCatalog.Aggregates;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.Entities
{
    public class SalesRecord : Entity<long>
    {
        public Book Book { get; private set; }
        public DateTime Date { get; private set; }
        public int QuantitySold { get; private set; }
        public Money Revenue { get; private set; }

        public SalesRecord(long id, Book book, DateTime date, int quantitySold, Money revenue)
        {
            Id = id;
            Book = book;
            Date = date;
            QuantitySold = quantitySold;
            Revenue = revenue;
        }

        public void IncreaseQuantity(int quantity)
        {
            QuantitySold += quantity;
        }
    }
}
