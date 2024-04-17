using DomainDrivenSample.SalesAndCatalog.Aggregates;
using DomainDrivenSample.SalesAndCatalog.DomainEvents;
using DomainDrivenSample.SalesAndCatalog.Services;

namespace SolutionName.DomainDrivenSample.SalesAndCatalog.Handlers
{
    public class BookNegotiationHandler : Handles<BookNegotiationEvent>, IDomainService
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IBookRepository _bookRepository;

        public BookNegotiationHandler(
            IPublisherRepository publisherRepository,
            IAuthorRepository authorRepository,
            IBookRepository bookRepository
        )
        {
            _publisherRepository = publisherRepository;
            AuthorRepository = authorRepository;
            _bookRepository = bookRepository;
        }

        public IAuthorRepository AuthorRepository { get; }

        public async Task Handle(
            BookNegotiationEvent args,
            CancellationToken cancellationToken = default
        )
        {
            var publisher = _publisherRepository.FindById(args.PublisherId);
            if (publisher is null)
                throw new InvalidOperationException("Publisher not found");
            var book = _bookRepository.FindById(args.BookId);
            var author = AuthorRepository.FindById(args.AuthorId);
            if (book is null || author is null)
                throw new InvalidOperationException("Book or author not found");

            // contract negotiation
            var contract = publisher.DrawUpContract(author, book, ContractType.Royalty);
            publisher.Negotiate(contract, ContractType.Royalty);
        }
    }
}
