using DomainDrivenSample.SalesAndCatalog.Aggregates;
using DomainDrivenSample.SalesAndCatalog.Entities;
using NRules.Fluent.Dsl;

namespace DomainDrivenSample.SalesAndCatalog.Rules;

public class PromotionCodeValidationRule : Rule
{
    public override void Define()
    {
        Promotion promotion = null;
        Order order = null;
        string promotionCode = null; // Assume we get this from the order

        When()
            .Match<Promotion>(
                () => promotion,
                p => p.IsActive(),
                p => p.ValidateCode(promotionCode)
            )
            .Match<Order>(() => order, o => o.PromotionCode == promotionCode);

        Then().Do(ctx => ApplyPromotionToOrder(promotion, order));
    }
}
