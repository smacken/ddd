using DomainDrivenSample.SalesAndCatalog.DomainEvents;
using DomainDrivenSample.SalesAndCatalog.Entities;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.Aggregates
{
    public class Book : AggregateRoot<Guid>
    {
        public BookMetadata Metadata { get; private set; }
        public List<Edition> Editions { get; private set; }
        public bool IsPublished => Editions.Any();

        public Book(Guid id, BookMetadata metadata)
            : base(id)
        {
            Metadata = metadata;
            Editions = new List<Edition>();
        }

        public void AddEdition(Edition edition)
        {
            Editions.Add(edition);
        }

        public void RemoveEdition(Edition edition)
        {
            Editions.Remove(edition);
        }

        public void UpdateMetadata(BookMetadata metadata)
        {
            Metadata = metadata;
        }

        public void UpdateEdition(Edition edition)
        {
            int index = Editions.FindIndex(0, x => x.Id == edition.Id);
            if(index >= 0)
            {
                Editions[index] = edition;
            }
        }

        public void RemoveEdition(Guid editionId)
        {
            var existingEdition = Editions.FirstOrDefault(e => e.Id == editionId);
            if (existingEdition == null)
            {
                throw new InvalidOperationException("Edition not found");
            }

            Editions.Remove(existingEdition);
        }

        public void Publish()
        {
            if (IsPublished)
                return;

            var bookPublishedEvent = new BookPublishedEvent(
                Id,
                Metadata.Title,
                new Dictionary<string, object>()
                {
                    {"Title", Metadata.Title},
                    {"Authors", Metadata.Authors},
                    {"ISBN", Metadata.ISBN},
                    {"Editions", Editions.Select(e => new {e.Id, e.Price, e.StockQuantity}).ToList()}
                }
            );
            AddDomainEvent(bookPublishedEvent);
        }
    }
}
