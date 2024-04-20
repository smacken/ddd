using NRules.Fluent.Dsl;
using DomainDrivenSample.SalesAndCatalog.Entities;
using DomainDrivenSample.SalesAndCatalog.Aggregates;

namespace DomainDrivenSample.SalesAndCatalog.Rules;

public class OrderPricingAdjustmentRule : Rule
{
    public override void Define()
    {
        Order order = null;
        Promotion appliedPromotion = null;

        When()
            .Match<Order>(() => order)
            .Let(() => appliedPromotion, () => order.);

        Then()
            .Do(ctx => AdjustOrderPricing(order, appliedPromotion));

    }
}

