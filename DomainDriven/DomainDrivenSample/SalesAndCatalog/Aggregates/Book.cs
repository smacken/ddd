using System.Collections.ObjectModel;
using DomainDrivenSample.SalesAndCatalog.DomainEvents;
using DomainDrivenSample.SalesAndCatalog.Entities;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.Aggregates
{
    public class Book : AggregateRoot<Guid>
    {
        private List<Edition> _editions = new List<Edition>();
        public BookMetadata Metadata { get; private set; }
        public ReadOnlyCollection<Edition> Editions => _editions.AsReadOnly();
        public bool IsPublished => _editions.Any();

        public Book(Guid id, BookMetadata metadata)
            : base(id)
        {
            Metadata = metadata;
            _editions = new List<Edition>();
        }

        public Book(BookMetadata metadata)
            : this(Guid.NewGuid(), metadata) { }

        public void AddEdition(Edition edition)
        {
            _editions.Add(edition);
        }

        public void RemoveEdition(Edition edition)
        {
            _editions.Remove(edition);
        }

        public void UpdateMetadata(BookMetadata metadata)
        {
            Metadata = metadata;
        }

        public void UpdateEdition(Edition edition)
        {
            var index = _editions.FindIndex(e => e.Id == edition.Id);
            if (index < 0)
                return;
            _editions[index] = edition;
        }

        public void RemoveEdition(Guid editionId)
        {
            var index = _editions.FindIndex(e => e.Id == editionId);
            if (index < 0)
                return;
            _editions.Remove(_editions[index]);
        }

        public void Publish()
        {
            if (IsPublished)
                return;

            DomainDriven.DomainEvents.Raise(
                new BookPublishedEvent(this.Id, this.Metadata.Title, this.Metadata.ToDictionary())
            );
        }
    }
}
