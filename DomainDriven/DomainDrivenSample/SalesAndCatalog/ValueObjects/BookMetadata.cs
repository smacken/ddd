using DomainDriven;

namespace DomainDrivenSample.SalesAndCatalog.ValueObjects
{
    public class BookMetadata : ValueObject
    {
        public string ISBN { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Genre { get; private set; }
        public List<string> Authors { get; private set; }

        public BookMetadata(
            string isbn,
            string title,
            string description,
            string genre,
            List<string> authors
        )
        {
            ISBN = isbn;
            Title = title;
            Description = description;
            Genre = genre;
            Authors = authors;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ISBN;
            yield return Title;
            yield return Description;
            yield return Genre;
            yield return Authors;
        }
    }
}
