namespace DomainDrivenSample.SalesAndCatalog.ValueObjects
{
    public class Isbn : ValueObject
    {
        public string Value { get; private set; }

        public Isbn(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
