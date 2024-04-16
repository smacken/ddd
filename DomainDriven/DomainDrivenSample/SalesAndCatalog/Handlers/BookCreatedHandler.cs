namespace DomainDrivenSample.SalesAndCatalog.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using DomainDriven;
    using DomainDrivenSample.SalesAndCatalog.DomainEvents;

    public class BookCreatedHandler : Handles<BookCreatedEvent>
    {
        public Task Handle(BookCreatedEvent args, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Book created: {args.Title}");
            return Task.CompletedTask;
        }
    }
}
