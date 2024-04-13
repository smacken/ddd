using DomainDriven;

namespace DomainDrivenSample.SalesAndCatalog.ValueObjects
{

    public class EmailAddress : ValueObject
    {
        public string Value { get; private set; }

        public EmailAddress(string value)
        {
            //validate email address
            
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}