using DomainDriven;
using DomainDrivenSample.SalesAndCatalog.Aggregates;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.Entities
{
    public class OrderItem : Entity<long>
    {
        public Book Book { get; private set; }
        public Edition Edition { get; private set; }
        public int Quantity { get; private set; }
        public Money Price { get; private set; }

        public OrderItem(long id, Book book, Edition edition, int quantity, Money price)
        {
            Id = id;
            Book = book;
            Edition = edition;
            Quantity = quantity;
            Price = price;
        }

        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public void DecreaseQuantity(int quantity)
        {
            Quantity -= quantity;
        }

        public void ChangeEdition(Edition newEdition)
        {
            Edition = newEdition;
        }

        public void ChangePrice(Money newPrice)
        {
            Price = newPrice;
        }

        public Money CalculateTotal()
        {
            return Price * Quantity;
        }
    }
}