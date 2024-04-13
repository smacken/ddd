using DomainDrivenSample.SalesAndCatalog.Entities;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.Services
{
    public interface IPricingService
    {
        Money CalculatePrice(Money basePrice, int quantity);
        Money CalculatePrice(Edition edition, int quantity);
    }

    public class PricingService : IPricingService
    {
        public Money CalculatePrice(Money basePrice, int quantity)
        {
            return basePrice * quantity;
        }

        public Money CalculatePrice(Edition edition, int quantity)
        {
            return CalculatePrice(edition.Price, quantity);
        }
    }
}
