namespace DomainDrivenSample.SalesAndCatalog.ValueObjects
{
    public class BookMetadata : ValueObject
    {
        public Isbn ISBN { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Genre { get; private set; }
        public string? Publisher { get; set; }
        public List<string> Authors { get; private set; }

        public BookMetadata(
            string isbn,
            string title,
            string description,
            string genre,
            List<string> authors
        )
        {
            ISBN = new Isbn(isbn);
            Title = title;
            Description = description;
            Genre = genre;
            Authors = authors;
        }

        public void RegisterBook(string publisher, Isbn isbn)
        {
            Publisher = publisher;
            ISBN = isbn;
        }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                { "ISBN", ISBN },
                { "Title", Title },
                { "Description", Description },
                { "Genre", Genre },
                { "Authors", Authors }
            };
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
