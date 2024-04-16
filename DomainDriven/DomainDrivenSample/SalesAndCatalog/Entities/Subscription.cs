using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.Entities
{
    public class Subscription : Entity<Guid>
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateRange DateRange { get; private set; }
        public SubscriptionType Type { get; private set; }

        public Subscription(Guid id, DateTime startDate, DateTime endDate, SubscriptionType type)
        {
            Id = id;
            Type = type;
            DateRange = new DateRange(startDate, endDate);
        }

        public void Renew(DateTime newEndDate)
        {
            DateRange.Extend(newEndDate);
        }

        public void ChangeType(SubscriptionType newType)
        {
            Type = newType;
        }

        public void Cancel()
        {
            DateRange.Cancel();
        }
    }
}
