namespace DomainDrivenSample.SalesAndCatalog.ValueObjexts;

public class DiscountType : ValueObject
{
    public static DiscountType Percentage => new DiscountType(1, nameof(Percentage));
    public static DiscountType Amount => new DiscountType(2, nameof(Amount));

    public int Id { get; private set; }
    public string Name { get; private set; }

    private DiscountType(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static DiscountType From(int id)
    {
        return id switch
        {
            1 => Percentage,
            2 => Amount,
            _
                => throw new InvalidOperationException(
                    $"Discount type with id {id} is not supported."
                )
        };
    }

    public static DiscountType From(string name)
    {
        return name switch
        {
            nameof(Percentage) => Percentage,
            nameof(Amount) => Amount,
            _
                => throw new InvalidOperationException(
                    $"Discount type with name {name} is not supported."
                )
        };
    }

    public override string ToString()
    {
        return Name;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
        yield return Name;
    }
}
