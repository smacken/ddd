using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.Entities
{
    public class Promotion : Entity<Guid>
    {
        public string ID { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateRange DateRange { get; private set; }
        public string Description { get; private set; }

        public Promotion(Guid id, DateTime startDate, DateTime endDate, string description)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            DateRange = new DateRange(startDate, endDate);
            Description = description;
        }

        public void ChangeDescription(string newDescription)
        {
            Description = newDescription;
        }

        public void ChangeDates(DateTime newStartDate, DateTime newEndDate)
        {
            StartDate = newStartDate;
            EndDate = newEndDate;
        }

        public bool IsActive()
        {
            return StartDate <= DateTime.Now && DateTime.Now <= EndDate;
        }

        public bool IsUpcoming()
        {
            return StartDate > DateTime.Now;
        }

        public bool IsExpired()
        {
            return EndDate < DateTime.Now;
        }

        public bool IsOngoing()
        {
            return IsActive() && !IsExpired();
        }

        public bool IsCancelled()
        {
            return EndDate < DateTime.Now;
        }

        public void Cancel()
        {
            EndDate = DateTime.Now;
        }

        public void Activate()
        {
            StartDate = DateTime.Now;
        }

        public void Deactivate()
        {
            EndDate = DateTime.Now;
        }

        public void Extend(DateTime newEndDate)
        {
            EndDate = newEndDate;
        }
    }
}
