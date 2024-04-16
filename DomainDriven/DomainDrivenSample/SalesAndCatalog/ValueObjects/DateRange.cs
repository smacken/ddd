using DomainDriven;

namespace DomainDrivenSample.SalesAndCatalog.ValueObjects;

public class DateRange : ValueObject
{
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    public DateRange(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            throw new ArgumentException("Start date must be before end date.");
        }

        StartDate = startDate;
        EndDate = endDate;
    }

    public bool IsActive()
    {
        return StartDate <= DateTime.Now && DateTime.Now <= EndDate;
    }

    public bool IsUpcoming()
    {
        return StartDate > DateTime.Now;
    }

    public bool IsExpired()
    {
        return EndDate < DateTime.Now;
    }

    public bool IsOngoing()
    {
        return IsActive() && !IsExpired();
    }

    public bool IsCancelled()
    {
        return EndDate < DateTime.Now;
    }

    public void Cancel()
    {
        EndDate = DateTime.Now;
    }

    public void Activate()
    {
        StartDate = DateTime.Now;
    }

    public void Deactivate()
    {
        EndDate = DateTime.Now;
    }

    public void Extend(DateTime newEndDate)
    {
        EndDate = newEndDate;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StartDate;
        yield return EndDate;
    }
}

