using DomainDrivenSample.SalesAndCatalog.Aggregates;

namespace DomainDrivenSample.SalesAndCatalog.DomainEvents
{
    public class BookNegotiationEvent : DomainEvent
    {
        public Guid BookId { get; }
        public Guid AuthorId { get; }
        public Guid PublisherId { get; }
        public ContractType ContractType { get; }

        public BookNegotiationEvent(
            Guid bookId,
            Guid authorId,
            Guid publisherId,
            ContractType contractType
        )
        {
            BookId = bookId;
            AuthorId = authorId;
            PublisherId = publisherId;
            ContractType = contractType;
        }

        public override void Flatten()
        {
            AddArg("BookId", BookId);
            AddArg("AuthorId", AuthorId);
            AddArg("PublisherId", PublisherId);
            AddArg("ContractType", ContractType);
        }
    }
}
