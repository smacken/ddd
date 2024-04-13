using DomainDriven;

namespace DomainDrivenSample.SalesAndCatalog.ValueObjects
{
    public class Money : ValueObject
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
        
        public Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }
        
        public static Money operator +(Money money1, Money money2)
        {
            if (money1.Currency != money2.Currency)
            {
                throw new InvalidOperationException("Cannot add money in different currencies");
            }
            return new Money(money1.Amount + money2.Amount, money1.Currency);
        }
        
        public static Money operator -(Money money1, Money money2)
        {
            if (money1.Currency != money2.Currency)
            {
                throw new InvalidOperationException("Cannot subtract money in different currencies");
            }
            return new Money(money1.Amount - money2.Amount, money1.Currency);
        }
        
        public static Money operator *(Money money, int multiplier)
        {
            return new Money(money.Amount * multiplier, money.Currency);
        }

        public static Money operator /(Money money, int divisor)
        {
            return new Money(money.Amount / divisor, money.Currency);
        }
        
        public static bool operator ==(Money money1, Money money2)
        {
            return money1.Equals(money2);
        }

        public static bool operator !=(Money money1, Money money2)
        {
            return !money1.Equals(money2);
        }

        public override string ToString()
        {
            return $"{Amount} {Currency}";
        }
    }

    
}