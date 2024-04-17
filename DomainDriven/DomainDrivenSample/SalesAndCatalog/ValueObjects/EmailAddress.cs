using DomainDriven;

namespace DomainDrivenSample.SalesAndCatalog.ValueObjects
{

    public class EmailAddress : ValueObject
    {
        public string Value { get; private set; }
        
        public EmailAddress(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
