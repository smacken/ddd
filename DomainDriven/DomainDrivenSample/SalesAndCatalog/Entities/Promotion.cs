using DomainDrivenSample.SalesAndCatalog.ValueObjects;
using DomainDrivenSample.SalesAndCatalog.ValueObjexts;

namespace DomainDrivenSample.SalesAndCatalog.Entities
{
    public class Promotion : Entity<Guid>
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateRange DateRange { get; private set; }
        public string Description { get; private set; }
        public DiscountType DiscountType { get; private set; } = DiscountType.Amount;
        public decimal DiscountValue { get; private set; }
        public bool RequiresCode { get; private set; }
        public List<string?> PromotionCodes { get; private set; }

        public Promotion(
            DateTime startDate,
            DateTime endDate,
            string description,
            decimal discountValue
        )
        {
            Id = Guid.NewGuid();
            StartDate = startDate;
            EndDate = endDate;
            DateRange = new DateRange(startDate, endDate);
            Description = description;
            PromotionCodes = new List<string?>();
            RequiresCode = false;
            DiscountValue = discountValue;
        }

        public void SetPromotionDetails(
            DiscountType discountType,
            decimal discountValue,
            bool requiresCode,
            List<string?> promotionCodes
        )
        {
            DiscountType = discountType;
            DiscountValue = discountValue;
            RequiresCode = requiresCode;
            PromotionCodes = promotionCodes;
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

        public bool ValidateCode(string promotionCode) =>
            RequiresCode && PromotionCodes.Contains(promotionCode);

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
