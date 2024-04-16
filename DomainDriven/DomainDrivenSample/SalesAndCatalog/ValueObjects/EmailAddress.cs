using DomainDriven;

namespace DomainDrivenSample.SalesAndCatalog.ValueObjects
{

    public class EmailAddress(string value) : ValueObject
    {
        public string Value { get; private set; } = value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
