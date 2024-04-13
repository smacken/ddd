using DomainDriven;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.Entities
{
    public class Subscription : Entity<Guid>
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public SubscriptionType Type { get; private set; }

        public Subscription(Guid id, DateTime startDate, DateTime endDate, SubscriptionType type)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            Type = type;
        }

        public void Renew(DateTime newEndDate)
        {
            EndDate = newEndDate;
        }

        public void ChangeType(SubscriptionType newType)
        {
            Type = newType;
        }

        public void Cancel()
        {
            EndDate = DateTime.Now;
        }
    }
}