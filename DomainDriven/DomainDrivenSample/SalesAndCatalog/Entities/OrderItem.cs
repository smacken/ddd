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

            // quantity should be greater than 0
            if (quantity <= 0)
            {
                throw new DomainException("Quantity should be greater than 0");
            }
        }

        public void Increase(int quantity) => Quantity += quantity;

        public void Decrease(int quantity) => Quantity -= quantity;

        public Money CalculateTotal() => Price * Quantity;

        public void ChangeEdition(Edition newEdition)
        {
            Edition = newEdition;
        }

        internal void ChangePrice(Money newPrice)
        {
            Price = newPrice;
        }

        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new DomainException("Quantity should be greater than 0");
            }
            Quantity = quantity;
        }
    }
}
