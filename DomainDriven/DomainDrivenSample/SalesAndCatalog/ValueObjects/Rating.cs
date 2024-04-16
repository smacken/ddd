namespace DomainDrivenSample.SalesAndCatalog.ValueObjects
{
    public class Rating : ValueObject
    {
        public int Value { get; private set; }

        public Rating(int value)
        {
            if (value < 1 || value > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5");
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
