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

        public static implicit operator string(Isbn isbn) => isbn.Value;

        //equals operator
        public static bool operator ==(Isbn left, Isbn right) => left.Equals(right);

        //not equals operator
        public static bool operator !=(Isbn left, Isbn right) => !left.Equals(right);
    }
}
