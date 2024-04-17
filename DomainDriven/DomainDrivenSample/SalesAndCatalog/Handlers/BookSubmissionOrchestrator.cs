using DomainDrivenSample.SalesAndCatalog.Aggregates;
using DomainDrivenSample.SalesAndCatalog.DomainEvents;
using DomainDrivenSample.SalesAndCatalog.Services;
using DomainDrivenSample.SalesAndCatalog.Specification;

namespace SolutionName.DomainDrivenSample.SalesAndCatalog.Handlers
{
    public class BookSubmissionOrchestrator : Handles<BookSubmittedEvent>, IDomainService
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IBookRegistry _bookRegistry;

        public BookSubmissionOrchestrator(
            IPublisherRepository publisherRepository,
            IBookRepository bookRepository,
            IBookRegistry bookRegistry
        )
        {
            _publisherRepository = publisherRepository;
            _bookRepository = bookRepository;
            _bookRegistry = bookRegistry;
        }

        public async Task Handle(
            BookSubmittedEvent args,
            CancellationToken cancellationToken = default
        )
        {
            var publisher = _publisherRepository
                .Find(new OfferablePublishesSpec())
                .FirstOrDefault();
            if (publisher is null)
                throw new InvalidOperationException("No publisher found");
            var registeredBook = _bookRegistry.Register(args.Book, publisher);
            await _bookRepository.Add(registeredBook, cancellationToken);

            // publisher submission
            DomainEvents.Raise(
                new BookNegotiationEvent(
                    args.Book.Id,
                    args.Author.Id,
                    publisher.Id,
                    ContractType.Royalty
                )
            );
        }
    }
}
