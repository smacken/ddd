using System.Collections.ObjectModel;
using DomainDrivenSample.SalesAndCatalog.DomainEvents;
using DomainDrivenSample.SalesAndCatalog.Entities;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.Aggregates
{
    public class Reader : AggregateRoot<Guid>
    {
        private readonly string _name;
        private readonly List<Review> _reviews = new();
        private readonly List<Subscription> _subscriptions = new();
        public string Name { get; private set; }
        public ReadOnlyCollection<Subscription> Subscriptions => _subscriptions.AsReadOnly();
        public ReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();

        public Reader(Guid id, string name): base(id)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
        }
        
        public void WriteReview(Book book, string content, Rating rating)
        {
            ReviewBookEvent review = new(this, book, content, rating);
            AddDomainEvent(review);
        }
    }
}
