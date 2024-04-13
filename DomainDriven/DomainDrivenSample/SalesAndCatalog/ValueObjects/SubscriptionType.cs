namespace DomainDrivenSample.SalesAndCatalog.ValueObjects
{
    public enum SubscriptionType
    {
        Author,
        Genre,
        Premium
    }

    public enum EditionType
    {
        Digital,
        Paperback,
        Hardcover
    }

    public enum OrderStatus
    {
        Pending,
        Placed,
        Submitted,
        Paid,
        Shipped,
        Delivered,
        Cancelled,
        Archived,
        Deleted,
        Completed,
        Returned,
        Refunded
    }

    public enum ServiceType
    {
        Editorial,
        CoverDesign,
        Marketing
    }
}
