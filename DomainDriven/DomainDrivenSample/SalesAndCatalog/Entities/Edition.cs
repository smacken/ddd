using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.Entities
{
    public class Edition : Entity<Guid>
    {
        public EditionType Type { get; private set; }
        public Money Price { get; private set; }
        public int StockQuantity { get; private set; }

        public Edition(Guid id, EditionType type, Money price, int stockQuantity)
        {
            Id = id;
            Type = type;
            Price = price;
            StockQuantity = stockQuantity;
        }

        public void ChangePrice(Money newPrice)
        {
            Price = newPrice;
        }

        public void IncreaseStock(int quantity)
        {
            StockQuantity += quantity;
        }

        public void DecreaseStock(int quantity)
        {
            StockQuantity -= quantity;
        }

        public bool IsAvailable(int quantity)
        {
            return StockQuantity >= quantity;
        }

        public void Sell(int quantity)
        {
            StockQuantity -= quantity;
        }
    }
}
