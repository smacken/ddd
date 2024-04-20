namespace DomainDrivenSample.SalesAndCatalog.ValueObjects
{
    public class PromotionCode : ValueObject
    {
        public string Code { get; private set; }
        public bool IsUsed { get; private set; }
        public bool IsSingleUse { get; private set; }

        public PromotionCode(string code, bool isSingleUse = false)
        {
            Code = code.Trim().ToUpper();
            IsUsed = false;
            IsSingleUse = isSingleUse;
        }

        public void MarkAsUsed()
        {
            IsUsed = true;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }

        public static PromotionCode Generate()
        {
            return new PromotionCode(Guid.NewGuid().ToString().Substring(0, 8));
        }

        public static implicit operator string(PromotionCode code) => code.Code;

        public static bool operator ==(PromotionCode left, PromotionCode right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PromotionCode left, PromotionCode right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return Code;
        }
    }
}
