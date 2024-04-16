namespace DomainDriven;

public interface ISlowlyChanging
{
    DateTime CreatedAt { get; set; }

    DateTime UpdatedAt { get; set; }

    DateTime ValidFrom { get; set; }

    DateTime? ValidTo { get; set; }

    bool IsCurrent() =>
        this.ValidFrom <= DateTime.Now
        && (this.ValidTo >= DateTime.Now || this.ValidTo == DateTime.MinValue);

    void MarkAsCurrent() => this.ValidTo = DateTime.Now;

    void Archive() => this.ValidTo = DateTime.UtcNow;

    void Restore() => this.ValidTo = DateTime.MinValue;
}

public interface IAudit
{
    string CreatedBy { get; set; }

    string UpdatedBy { get; set; }
}
